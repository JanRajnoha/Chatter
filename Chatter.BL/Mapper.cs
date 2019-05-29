using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Chatter.BL.DTO.Group;
using Chatter.BL.DTO.Post;
using Chatter.DAL.Model;
using Chatter.BL.DTO.User;
using System.Linq;
using Chatter.BL.DTO.Organization;
using Chatter.BL.DTO.LogIn;
using Chatter.BL.DTO.File;
using Chatter.BL.DTO.Comment;
using Chatter.BL.DTO.Tag;

namespace Chatter.BL
{
    public class Mapper
    {
        public PostListModelDTO MapEntityToListModel(Post post)
        {
            return new PostListModelDTO()
            {
                Id = post.Id,
                PostHeader = post.PostHeader,
                DateTime = post.DateTime
            };
        }

        public PostDetailModelDTO MapEntityToDetailModel(Post post)
        {
            return new PostDetailModelDTO()
            {
                Id = post.Id,
                PostHeader = post.PostHeader,
                Content = post.Content,
                DateTime = post.DateTime,
                User = MapEntityToDetailModel(post.User),
                Comments = new ObservableCollection<CommentDetailModelDTO>(post.Comments.Select(x =>
                {
                    var res = MapEntityToDetailModel(x);
                    res.Post = MapEntityToListModel(post);
                    return res;
                }))
            };
        }

        public Post MapDetailToEntity(PostDetailModelDTO post)
        {
            if (post == null)
                return new Post();

            return new Post()
            {
                Id = post.Id,
                PostHeader = post.PostHeader,
                Content = post.Content,
                DateTime = post.DateTime,
                User = MapDetailToEntity(post.User),
                Comments = new ObservableCollection<Comment>(post.Comments.Select(MapDetailToEntity))
            };
        }

        public GroupListModelDTO MapEntityToListModel(Group group)
        {
            return new GroupListModelDTO()
            {
                Id = group.Id,
                Name = group.Name
            };
        }

        public GroupDetailModelDTO MapEntityToDetailModel(Group group)
        {
            return new GroupDetailModelDTO()
            {
                Id = group.Id,
                Name = group.Name,
                Description = group.Description,
                Posts = new ObservableCollection<PostDetailModelDTO>(group.Posts.Select(MapEntityToDetailModel)),
                Admins = new ObservableCollection<UserListModelDTO>(group.Admins.Select(x => MapEntityToListModel(x.Admin))),
                Users = new ObservableCollection<UserListModelDTO>(group.Users.Select(x => MapEntityToListModel(x.User)))
            };
        }

        public Group MapDetailToEntity(GroupDetailModelDTO group)
        {
            if (group == null)
                return new Group();

            return new Group()
            {
                Id = group.Id,
                Name = group.Name,
                Description = group.Description,
                Posts = new ObservableCollection<Post>(group.Posts.Select(MapDetailToEntity))
            };
        }

        public UserListModelDTO MapEntityToListModel(User user)
        {
            return new UserListModelDTO()
            {
                Id = user.Id,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }

        public UserDetailModelDTO MapEntityToDetailModel(User user)
        {
            if (user == null)
                return new UserDetailModelDTO();

            return new UserDetailModelDTO()
            {
                Id = user.Id,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Gender = user.Gender,
                Description = user.Description,
                Email = user.Email,
                Hash = user.Password,
                Salt = user.Salt,
                RegistrationDate = user.RegistrationDate,
                Groups = new ObservableCollection<GroupListModelDTO>(user.Groups.Select(x => MapEntityToListModel(x.Group))),
                Organizations = new ObservableCollection<OrganizationListModelDTO>(user.Organizations.Select(x => MapEntityToListModel(x.Organization))),
                SignInLogs = new ObservableCollection<LogInDetailModelDTO>(user.SignInLogs.Select(MapEntityToDetailModel)),
                AdminGroups = new ObservableCollection<GroupListModelDTO>(user.AdminGroups.Select(x => MapEntityToListModel(x.Group))),
                AdminOrganizations = new ObservableCollection<OrganizationListModelDTO>(user.AdminOrganizations.Select(x => MapEntityToListModel(x.Organization)))
            };
        }

        public User MapDetailToEntity(UserDetailModelDTO user)
        {
            if (user == null)
                return new User()
                {
                    Email = Functions.RandomString(6)
                };

            if (user.Email == null)
                user.Email = Functions.RandomString(5);

            byte[] Salt, Hash;

            if (user.Password == null)
            {
                Salt = user.Salt;
                Hash = user.Hash;
            }
            else
                (Salt, Hash) = Crypto.Encrypt(user.Password);


            return new User()
            {
                Id = user.Id,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Gender = user.Gender,
                Description = user.Description,
                Email = user.Email,
                Password = Hash,
                Salt = Salt,
                RegistrationDate = user.RegistrationDate,
                SignInLogs = new ObservableCollection<SignInLog>(user.SignInLogs.Select(MapDetailToEntity)),
            };
        }

        public CommentListModelDTO MapEntityToListModel(Comment comment)
        {
            return new CommentListModelDTO()
            {
                Id = comment.Id,
                Content = comment.Content,
                DateTime = comment.DateTime
            };
        }

        public CommentDetailModelDTO MapEntityToDetailModel(Comment comment)
        {
            return new CommentDetailModelDTO()
            {
                Id = comment.Id,
                Content = comment.Content,
                DateTime = comment.DateTime,
                User = MapEntityToDetailModel(comment.User)
            };
        }

        public Comment MapDetailToEntity(CommentDetailModelDTO comment)
        {
            if (comment == null)
                return new Comment();

            return new Comment()
            {
                Content = comment.Content,
                DateTime = comment.DateTime,
                Id = comment.Id,
                User = MapDetailToEntity(comment.User)
            };
        }

        public FileListModelDTO MapEntityToListModel(File file)
        {
            return new FileListModelDTO()
            {
                Id = file.Id,
                Name = file.Name,
                Size = file.Size
            };
        }

        public FileDetailModelDTO MapEntityToDetailModel(File file)
        {
            return new FileDetailModelDTO()
            {
                Id = file.Id,
                Name = file.Name,
                Size = file.Size,
                Comment = MapEntityToDetailModel(file.Comment),
                Post = MapEntityToDetailModel(file.Post),
                FileType = file.Type,
                Path = file.Path,
                UploadedBy = MapEntityToDetailModel(file.UploadedBy)
            };
        }

        public File MapDetailToEntity(FileDetailModelDTO file)
        {
            if (file == null)
                return new File();

            return new File()
            {
                Id = file.Id,
                Comment = MapDetailToEntity(file.Comment),
                Name = file.Name,
                Path = file.Path,
                Post = MapDetailToEntity(file.Post),
                Size = file.Size,
                Type = file.FileType,
                UploadedBy = MapDetailToEntity(file.UploadedBy)
            };
        }

        public LogInDetailModelDTO MapEntityToDetailModel(SignInLog login)
        {
            return new LogInDetailModelDTO()
            {
                Id = login.Id,
                SignInLogTime = login.SignInLogTime,
                SignLogCode = login.SignLogCode
            };
        }

        public LogInListModelDTO MapEntityToListModel(SignInLog login)
        {
            return new LogInListModelDTO()
            {
                Id = login.Id
            };
        }

        public SignInLog MapDetailToEntity(LogInDetailModelDTO login)
        {
            if (login == null)
                return new SignInLog();

            return new SignInLog()
            {
                Id = login.Id,
                SignLogCode = login.SignLogCode,
                SignInLogTime = login.SignInLogTime,
                UserId = login.User.Id
            };
        }

        public OrganizationListModelDTO MapEntityToListModel(Organization organization)
        {
            return new OrganizationListModelDTO()
            {
                Id = organization.Id,
                Name = organization.Name
            };
        }

        public OrganizationDetailModelDTO MapEntityToDetailModel(Organization organization)
        {
            return new OrganizationDetailModelDTO()
            {
                Id = organization.Id,
                Name = organization.Name,
                Admins = new ObservableCollection<UserListModelDTO>(organization.Admins.Select(x => MapEntityToListModel(x.Admin))),
                Description = organization.Description,
                Groups = new ObservableCollection<GroupDetailModelDTO>(organization.Groups.Select(x =>
                {
                    var res = MapEntityToDetailModel(x);
                    res.Organization = MapEntityToListModel(organization);
                    return res;
                })),
                Users = new ObservableCollection<UserListModelDTO>(organization.Users.Select(x => MapEntityToListModel(x.User)))
            };
        }

        public Organization MapDetailToEntity(OrganizationDetailModelDTO organzation)
        {
            if (organzation == null)
                return new Organization();

            return new Organization()
            {
                Description = organzation.Description,
                Groups = new ObservableCollection<Group>(organzation.Groups.Select(MapDetailToEntity)),
                Id = organzation.Id,
                Name = organzation.Name
            };
        }

        public TagDetailModelDTO MapEntityToDetailModel(Tag tag)
        {
            return new TagDetailModelDTO()
            {
                Id = tag.Id,
                Post = MapEntityToDetailModel(tag.Post),
                Comment = MapEntityToDetailModel(tag.Comment),
                User = MapEntityToDetailModel(tag.User)
            };
        }

        public Tag MapDetailToEntity(TagDetailModelDTO tag)
        {
            if (tag == null)
                return new Tag();

            return new Tag()
            {
                Id = tag.Id,
                User = MapDetailToEntity(tag.User),
                Comment = MapDetailToEntity(tag.Comment),
                Post = MapDetailToEntity(tag.Post)
            };
        }
    }
}