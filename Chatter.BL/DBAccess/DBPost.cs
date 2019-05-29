using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chatter.BL.DBAccess.Interfaces;
using Chatter.BL.DTO.Post;
using Chatter.DAL.Data;
using Chatter.DAL.Model;
using Chatter.BL.DBAccess;
using Microsoft.EntityFrameworkCore;
using Chatter.BL.DTO.Comment;
using Chatter.DAL.Factories;

namespace Chatter.BL.DBAccess
{
    public class DBPost : IDBPost
    {
        private readonly IDBContextFactory chatterDbContextFactory;

        public DBPost(IDBContextFactory chatterDbContextFactory)
        {
            this.chatterDbContextFactory = chatterDbContextFactory;
        }

        readonly Mapper mapper = new Mapper();

        public PostDetailModelDTO AddPost(PostDetailModelDTO post)
        {
            using (var connection = chatterDbContextFactory.CreateDbContext())
            {
                var newPost = mapper.MapDetailToEntity(post);

                newPost.Id = Guid.NewGuid();
                newPost.DateTime = DateTime.Now;

                connection.Users.Attach(newPost.User);
                connection.Posts.Add(newPost);
                connection.SaveChanges();
                return mapper.MapEntityToDetailModel(newPost);
            }
        }

        public PostDetailModelDTO GetPostById(Guid Id)
        {
            using (var connection = chatterDbContextFactory.CreateDbContext())
            {
                var post = connection.Posts.FirstOrDefault(t => t.Id == Id);
                return mapper.MapEntityToDetailModel(post);
            }
        }

        public List<PostDetailModelDTO> GetPostDetailList(Guid groupId)
        {
            using (var connection = chatterDbContextFactory.CreateDbContext())
            {
                var group = connection.Groups.Where(x => x.Id == groupId).FirstOrDefault();

                if (group == null)
                    return new List<PostDetailModelDTO>();

                return group.Posts.Select(mapper.MapEntityToDetailModel).ToList();
            }
        }

        public List<PostDetailModelDTO> GetPostList()
        {
            using (var connection = chatterDbContextFactory.CreateDbContext())
            {
                return connection.Posts
                    .Select(e => mapper.MapEntityToDetailModel(e))
                    .ToList();
            }
        }

        public void RemovePost(Guid Id)
        {
            using (var connection = chatterDbContextFactory.CreateDbContext())
            {
                var post = connection.Posts.FirstOrDefault(t => t.Id == Id);

                if (post == null)
                {
                    return;
                }

                connection.Remove(post);
                connection.SaveChanges();
            }
        }

        public PostDetailModelDTO FindPostById(Guid Id)
        {
            using (var connection = chatterDbContextFactory.CreateDbContext())
            {
                var entity = connection.Posts.First(t => t.Id == Id);
                return mapper.MapEntityToDetailModel(entity);
            }
        }

        public void Update(PostDetailModelDTO post)
        {
            using (var connection = chatterDbContextFactory.CreateDbContext())
            {
                var entity = mapper.MapDetailToEntity(post);
                connection.Posts.Update(entity);
                connection.SaveChanges();
            }
        }

        public PostDetailModelDTO GetPostByComment(CommentDetailModelDTO comment)
        {
            using (var connection = chatterDbContextFactory.CreateDbContext())
            {
                return mapper.MapEntityToDetailModel(connection.Posts.Where(x => x.Comments.Where(y => x.Id == y.Id).FirstOrDefault() != null).FirstOrDefault());
            }
        }
    }
}
