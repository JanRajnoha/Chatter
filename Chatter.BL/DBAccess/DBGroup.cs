using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Chatter.BL.DBAccess.Interfaces;
using Chatter.BL.DTO.Group;
using Chatter.BL.DTO.Organization;
using Chatter.DAL.Data;
using Chatter.DAL.Model;
using Remotion.Linq.Parsing;
using Chatter.DAL.Model.ManyToManyTables;
using Chatter.BL.DTO.Post;
using Chatter.DAL.Factories;
using Microsoft.EntityFrameworkCore;

namespace Chatter.BL.DBAccess
{
    public class DBGroup : IDBGroup
    {
        private readonly IDBContextFactory chatterDbContextFactory;

        public DBGroup(IDBContextFactory chatterDbContextFactory)
        {
            this.chatterDbContextFactory = chatterDbContextFactory;
        }

        readonly Mapper mapper = new Mapper();

        public GroupDetailModelDTO AddGroup(GroupDetailModelDTO group)
        {
            using (var connection = chatterDbContextFactory.CreateDbContext())
            {
                var newGroup = mapper.MapDetailToEntity(group);

                foreach (var admin in group.Admins)
                {
                    var adminDet = connection.Users.Single(x => x.Id == admin.Id);
                    connection.Users.Attach(adminDet);

                    connection.Admin_Group.Add(new Admin_Group()
                    {
                        Id = Guid.NewGuid(),
                        Admin = adminDet,
                        AdminRefId = adminDet.Id,
                        Group = newGroup,
                        GroupRefId = newGroup.Id
                    });
                }

                foreach (var user in group.Users)
                {
                    var userDet = connection.Users.Single(x => x.Id == user.Id);
                    connection.Users.Attach(userDet);

                    connection.User_Group.Add(new User_Group()
                    {
                        Id = Guid.NewGuid(),
                        User = userDet,
                        UserRefId = userDet.Id,
                        Group = newGroup,
                        GroupRefId = newGroup.Id
                    });
                }

                connection.Groups.Add(newGroup);
                connection.SaveChanges();
                return mapper.MapEntityToDetailModel(newGroup);
            }
        }

        public void EditGroup(GroupDetailModelDTO group)
        {
            using (var connection = chatterDbContextFactory.CreateDbContext())
            {
                Group edit_group = new Group() { Id = group.Id };
                edit_group = connection.Groups.Where(x => x.Id == group.Id).SingleOrDefault();
                connection.Groups.Attach(edit_group);

                edit_group.Description = group.Description;
                edit_group.Name = group.Name;

                foreach (var admin in group.Admins)
                {
                    if (connection.Admin_Group.SingleOrDefault(x => x.AdminRefId == admin.Id && x.GroupRefId == edit_group.Id) == null)
                    {
                        var adminDet = connection.Users.Single(x => x.Id == admin.Id);
                        connection.Users.Attach(adminDet);

                        connection.Admin_Group.Add(new Admin_Group()
                        {
                            Id = Guid.NewGuid(),
                            Group = edit_group,
                            GroupRefId = edit_group.Id,
                            Admin = adminDet,
                            AdminRefId = adminDet.Id
                        });
                    }
                    else
                        connection.Admin_Group.Attach(connection.Admin_Group.Single(x => x.AdminRefId == admin.Id && x.GroupRefId == edit_group.Id));
                }

                foreach (var user in group.Users)
                {
                    if (connection.User_Group.SingleOrDefault(x => x.UserRefId == user.Id && x.GroupRefId == edit_group.Id) == null)
                    {
                        var userDet = connection.Users.Single(x => x.Id == user.Id);
                        connection.Users.Attach(userDet);

                        connection.User_Group.Add(new User_Group()
                        {
                            Id = Guid.NewGuid(),
                            Group = edit_group,
                            GroupRefId = edit_group.Id,
                            User = userDet,
                            UserRefId = userDet.Id
                        });
                    }
                    else
                        connection.User_Group.Attach(connection.User_Group.Single(x => x.UserRefId == user.Id && x.GroupRefId == edit_group.Id));
                }

                foreach (var post in group.Posts)
                {
                    connection.Users.Attach(connection.Users.Single(x => x.Id == post.User.Id));

                    if (connection.Groups.Include(x => x.Posts).Single(x => x.Id == group.Id).Posts.FirstOrDefault(x => x.Id == post.Id) != null)
                        if (edit_group.Posts.Select(x => x.Id).Contains(post.Id))
                            continue;
                        else
                            connection.Posts.Attach(mapper.MapDetailToEntity(post));
                    else
                        edit_group.Posts.Add(mapper.MapDetailToEntity(post));
                }

                connection.SaveChanges();
            }
        }

        public GroupDetailModelDTO GetGroupById(Guid Id)
        {
            using (var connection = chatterDbContextFactory.CreateDbContext())
            {
                var group = connection.Groups.Include(x => x.Admins).ThenInclude(x => x.Admin).Include(x => x.Users).ThenInclude(x => x.User).Include(x => x.Posts).SingleOrDefault(t => t.Id == Id);

                if (group == null)
                    return new GroupDetailModelDTO();

                return mapper.MapEntityToDetailModel(group);
            }
        }

        public List<GroupDetailModelDTO> GetGroupDetailList()
        {
            using (var connection = chatterDbContextFactory.CreateDbContext())
            {
                return connection.Groups.Include(x => x.Admins).ThenInclude(x => x.Admin).Include(x => x.Users).ThenInclude(x => x.User).Include(x => x.Posts).Select(mapper.MapEntityToDetailModel).ToList();
            }
        }

        public List<GroupListModelDTO> GetGroupList()
        {
            using (var connection = chatterDbContextFactory.CreateDbContext())
            {
                return connection.Groups.Select(mapper.MapEntityToListModel).ToList();
            }
        }

        //public List<Group> GetGroupList(Guid OrgId)
        //{
        //    using (var connection = chatterDbContextFactory.CreateDbContext())
        //    {
        //        return connection.Groups.ToList();
        //    }
        //}

        public void RemoveGroup(Guid Id)
        {
            using (var connection = chatterDbContextFactory.CreateDbContext())
            {
                var group = connection.Groups.FirstOrDefault(t => t.Id == Id);

                if (group == null)
                {
                    return;
                }

                connection.Remove(group);
                connection.SaveChanges();
            }
        }

        public GroupDetailModelDTO FindGroupById(Guid Id)
        {
            using (var connection = chatterDbContextFactory.CreateDbContext())
            {
                var entity = connection.Groups.First(t => t.Id == Id);
                return mapper.MapEntityToDetailModel(entity);
            }
        }

        public void UpdateGroup(GroupDetailModelDTO group)
        {
            using (var connection = chatterDbContextFactory.CreateDbContext())
            {
                var entity = mapper.MapDetailToEntity(group);
                connection.Groups.Update(entity);
                connection.SaveChanges();
            }
        }

        public List<PostDetailModelDTO> GetPosts(Guid groupId)
        {
            using (var connection = chatterDbContextFactory.CreateDbContext())
            {
                var group = connection.Groups.Include(x => x.Posts).ThenInclude(x => x.User).SingleOrDefault(x => x.Id == groupId);

                if (group == null)
                    return new List<PostDetailModelDTO>();

                return group.Posts.Select(mapper.MapEntityToDetailModel).ToList();
            }
        }
    }
}
