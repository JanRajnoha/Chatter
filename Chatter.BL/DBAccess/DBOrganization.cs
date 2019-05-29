using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Chatter.BL.DBAccess.Interfaces;
using Chatter.BL.DTO.Organization;
using Chatter.DAL.Data;
using Chatter.DAL.Model;
using Chatter.DAL.Model.ManyToManyTables;
using Chatter.BL.DTO.Group;
using Chatter.BL.DTO.User;
using Chatter.DAL.Factories;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace Chatter.BL.DBAccess
{
    public class DBOrganization : IDBOrganization
    {
        private readonly IDBContextFactory chatterDbContextFactory;

        public DBOrganization(IDBContextFactory chatterDbContextFactory)
        {
            this.chatterDbContextFactory = chatterDbContextFactory;
        }

        readonly Mapper mapper = new Mapper();

        public OrganizationDetailModelDTO AddOrganization(OrganizationDetailModelDTO organization)
        {
            using (var connection = chatterDbContextFactory.CreateDbContext())
            {
                var newOrganization = mapper.MapDetailToEntity(organization);

                foreach (var admin in organization.Admins)
                {
                    var adminDet = connection.Users.Single(x => x.Id == admin.Id);
                    connection.Users.Attach(adminDet);

                    connection.Admin_Organization.Add(new Admin_Organization()
                    {
                        Id = Guid.NewGuid(),
                        Admin = adminDet,
                        AdminRefId = adminDet.Id,
                        Organization = newOrganization,
                        OrganizationRefId = newOrganization.Id
                    });
                }

                foreach (var user in organization.Users)
                {
                    var userDet = connection.Users.Single(x => x.Id == user.Id);
                    connection.Users.Attach(userDet);

                    connection.User_Organization.Add(new User_Organization()
                    {
                        Id = Guid.NewGuid(),
                        User = userDet,
                        UserRefId = userDet.Id,
                        Organization = newOrganization,
                        OrganizationRefId = newOrganization.Id
                    });
                }

                connection.Organizations.Add(newOrganization);
                connection.SaveChanges();
                return mapper.MapEntityToDetailModel(newOrganization);
            }
        }

        public void EditOrganization(OrganizationDetailModelDTO organization)
        {
            using (var connection = chatterDbContextFactory.CreateDbContext())
            {
                Organization edit_organization = new Organization() { Id = organization.Id };
                edit_organization = connection.Organizations.Where(x => x.Id == organization.Id).SingleOrDefault();
                connection.Organizations.Attach(edit_organization);

                edit_organization.Description = organization.Description;
                edit_organization.Name = organization.Name;
                //edit_organization.Groups = organization.Groups.Select(mapper.MapDetailToEntity).ToList();

                foreach (var admin in organization.Admins)
                {
                    if (connection.Admin_Organization.FirstOrDefault(x => x.AdminRefId == admin.Id && x.OrganizationRefId == edit_organization.Id) == null)
                    {
                        var adminDet = connection.Users.Single(x => x.Id == admin.Id);
                        connection.Users.Attach(adminDet);

                        connection.Admin_Organization.Add(new Admin_Organization()
                        {
                            Id = Guid.NewGuid(),
                            Organization = edit_organization,
                            OrganizationRefId = edit_organization.Id,
                            Admin = adminDet,
                            AdminRefId = adminDet.Id
                        });
                    }
                    else
                        connection.Admin_Organization.Attach(connection.Admin_Organization.Single(x => x.AdminRefId == admin.Id && x.OrganizationRefId == edit_organization.Id));
                }

                foreach (var admin in connection.Admin_Organization.Include(x => x.Admin).Where(x => x.OrganizationRefId == edit_organization.Id && !edit_organization.Users.Select(y => y.Id).Contains(x.AdminRefId)))
                {
                    if (!organization.Admins.Select(x => x.Id).Contains(admin.AdminRefId))
                    {
                        connection.Users.Attach(admin.Admin);

                        connection.Admin_Organization.Remove(admin);
                    }
                }

                foreach (var user in organization.Users)
                {
                    if (connection.User_Organization.FirstOrDefault(x => x.UserRefId == user.Id && x.OrganizationRefId == edit_organization.Id) == null)
                    {
                        var userDet = connection.Users.Single(x => x.Id == user.Id);
                        connection.Users.Attach(userDet);

                        connection.User_Organization.Add(new User_Organization()
                        {
                            Id = Guid.NewGuid(),
                            Organization = edit_organization,
                            OrganizationRefId = edit_organization.Id,
                            User = userDet,
                            UserRefId = userDet.Id
                        });
                    }
                    else
                        connection.User_Organization.Attach(connection.User_Organization.Single(x => x.UserRefId == user.Id && x.OrganizationRefId == edit_organization.Id));
                }

                foreach (var user in connection.User_Organization.Include(x => x.User).Where(x => x.OrganizationRefId == edit_organization.Id && !edit_organization.Users.Select(y => y.Id).Contains(x.UserRefId)))
                {
                    if (!organization.Users.Select(x => x.Id).Contains(user.UserRefId))
                    {
                        connection.Users.Attach(user.User);

                        connection.User_Organization.Remove(user);
                    }
                }

                foreach (var group in organization.Groups)
                {
                    if (connection.Organizations.Include(x => x.Groups).Single(x => x.Id == organization.Id).Groups.SingleOrDefault(x => x.Id == group.Id) == null)
                        edit_organization.Groups.Add(mapper.MapDetailToEntity(group));
                }

                connection.SaveChanges();
            }
        }

        public OrganizationDetailModelDTO GetOrganizationById(Guid Id)
        {
            using (var connection = chatterDbContextFactory.CreateDbContext())
            {
                var organization_id = connection.Organizations.Include(x => x.Admins).ThenInclude(x => x.Admin).Include(x => x.Users).ThenInclude(x => x.User).Include(x => x.Groups).First(x => x.Id == Id);
                return mapper.MapEntityToDetailModel(organization_id);
            }
        }

        public List<OrganizationListModelDTO> GetOrganizationList()
        {
            using (var connection = chatterDbContextFactory.CreateDbContext())
            {
                return connection.Organizations.Select(mapper.MapEntityToListModel).ToList();
            }
        }

        public List<OrganizationDetailModelDTO> GetOrganizationFullList()
        {
            using (var connection = chatterDbContextFactory.CreateDbContext())
            {
                return connection.Organizations.Include(x => x.Admins).ThenInclude(x => x.Admin).Include(x => x.Users).ThenInclude(x => x.User).Include(x => x.Groups).Select(mapper.MapEntityToDetailModel).ToList();
            }
        }

        public List<OrganizationListModelDTO> GetOrganizationListByUser(Guid userId)
        {
            using (var connection = chatterDbContextFactory.CreateDbContext())
            {
                return connection.Organizations.Where(x => x.Users.FirstOrDefault(y => y.Id == userId) != null || x.Admins.FirstOrDefault(y => y.Id == userId) != null).Include(x => x.Groups).Select(mapper.MapEntityToListModel).ToList();
            }
        }

        public List<OrganizationDetailModelDTO> GetOrganizationFullListByUser(Guid userId)
        {
            using (var connection = chatterDbContextFactory.CreateDbContext())
            {
                var organizations = connection.Organizations.Include(x => x.Admins).ThenInclude(x => x.Admin).Include(x => x.Users).ThenInclude(x => x.User).Where(x => x.Users.FirstOrDefault(y => y.UserRefId == userId) != null || x.Admins.FirstOrDefault(y => y.AdminRefId == userId) != null).Include(x => x.Groups).ThenInclude(x => x.Admins).Include(x => x.Groups).ThenInclude(x => x.Users).Select(mapper.MapEntityToDetailModel).ToList();

                foreach (var organization in organizations)
                {
                    organization.Groups = organization.Groups.Where(x => x.Admins.Select(y => y.Id).Contains(userId) || x.Users.Select(y => y.Id).Contains(userId)).ToList();

                    if (organization.Groups == null)
                        organization.Groups = new ObservableCollection<GroupDetailModelDTO>();
                }

                return organizations;
            }
        }

        public List<GroupListModelDTO> GetGroupList(Guid orgId)
        {
            using (var connection = chatterDbContextFactory.CreateDbContext())
            {
                return (connection.Organizations.Include(x => x.Admins).ThenInclude(x => x.Admin).Include(x => x.Users).ThenInclude(x => x.User).Include(x => x.Groups).SingleOrDefault(x => x.Id == orgId))?.Groups.Select(mapper.MapEntityToListModel).ToList();
            }
        }

        public void RemoveOrganization(Guid Id)
        {
            using (var connection = chatterDbContextFactory.CreateDbContext())
            {
                var organization = connection.Organizations.Include(x => x.Groups).First(x => x.Id == Id);
                connection.Remove(organization);
                connection.SaveChanges();
            }
        }

        public void AddUserToOrganization(OrganizationDetailModelDTO organization, UserDetailModelDTO user)
        {
            using (var connection = chatterDbContextFactory.CreateDbContext())
            {
                Organization edit_organization = new Organization() { Id = organization.Id };
                User_Organization newUser = new User_Organization() { Id = user.Id };

                edit_organization.Users.Add(newUser);

                connection.SaveChanges();
            }
        }

        public void RemoveOrganizationUser(OrganizationDetailModelDTO organization, Guid usrId)
        {
            using (var connection = chatterDbContextFactory.CreateDbContext())
            {
                var user = connection.User_Organization.First(x => x.Id == usrId);
                Organization edit_organization = new Organization() { Id = organization.Id };

                edit_organization.Users.Remove(user);

                connection.SaveChanges();
            }
        }

        public OrganizationDetailModelDTO GetOrganizationByGroupId(Guid groupId)
        {
            using (var connection = chatterDbContextFactory.CreateDbContext())
            {
                var organization = connection.Organizations.Include(x => x.Admins).ThenInclude(x => x.Admin).Include(x => x.Users).ThenInclude(x => x.User).Include(x => x.Groups).SingleOrDefault(x => x.Groups.Select(y => y.Id).Contains(groupId));

                return organization == null ? new OrganizationDetailModelDTO() : mapper.MapEntityToDetailModel(organization);
            }
        }
    }
}
