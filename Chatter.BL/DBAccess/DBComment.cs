using Chatter.BL.DBAccess.Interfaces;
using Chatter.BL.DTO.Comment;
using Chatter.BL.DTO.Post;
using Chatter.BL.DTO.User;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Chatter.DAL.Factories;
using Microsoft.EntityFrameworkCore;

namespace Chatter.BL.DBAccess
{
    public class DBComment : IDBComment
    {
        private readonly IDBContextFactory chatterDbContextFactory;

        public DBComment(IDBContextFactory chatterDbContextFactory)
        {
            this.chatterDbContextFactory = chatterDbContextFactory;
        }

        readonly Mapper mapper = new Mapper();

        public CommentDetailModelDTO AddComment(CommentDetailModelDTO comment)
        {
            using (var connection = chatterDbContextFactory.CreateDbContext())
            {
                var newComment = mapper.MapDetailToEntity(comment);

                connection.Users.Attach(mapper.MapDetailToEntity(comment.User));

                var post = connection.Posts.Single(x => x.Id == comment.Post.Id);
                newComment.Post = post;
                connection.Posts.Attach(post);

                connection.Comments.Add(newComment);
                connection.SaveChanges();
                return mapper.MapEntityToDetailModel(newComment);
            }
        }

        public bool EditComment(CommentDetailModelDTO comment)
        {
            throw new NotImplementedException();
        }

        public ICollection<CommentDetailModelDTO> GetAllComments()
        {
            using (var connection = chatterDbContextFactory.CreateDbContext())
            {
                return new ObservableCollection<CommentDetailModelDTO>(connection.Comments.Select(mapper.MapEntityToDetailModel));
            }
        }

        public CommentDetailModelDTO GetCommentByID(Guid id)
        {
            using (var connection = chatterDbContextFactory.CreateDbContext())
            {
                return connection.Comments.Where(x => x.Id == id).Select(mapper.MapEntityToDetailModel).FirstOrDefault();
            }
        }

        public ICollection<CommentDetailModelDTO> GetCommentsByPostID(Guid postId)
        {
            using (var connection = chatterDbContextFactory.CreateDbContext())
            {
                var post = connection.Posts.Single(x => x.Id == postId);

                return new ObservableCollection<CommentDetailModelDTO>(connection.Comments.Where(x => x.Post == post).Include(x => x.User).Include(x => x.Post).Select(mapper.MapEntityToDetailModel));
            }
        }

        public ICollection<CommentDetailModelDTO> GetCommentsByUser(UserDetailModelDTO user)
        {
            using (var connection = chatterDbContextFactory.CreateDbContext())
            {
                var posts = new DBPost(chatterDbContextFactory);

                var comments = new ObservableCollection<CommentDetailModelDTO>(connection.Comments.Where(x => x.User.Id == user.Id).Select(mapper.MapEntityToDetailModel));

                foreach (var comment in comments)
                {
                    comment.Post = posts.GetPostByComment(comment);
                }

                return comments;
            }
        }

        public bool RemoveComment(CommentDetailModelDTO comment)
        {
            if (comment == null)
                return false;

            using (var connection = chatterDbContextFactory.CreateDbContext())
            {
                connection.Comments.Remove(mapper.MapDetailToEntity(comment));
                return connection.SaveChanges() == 1;
            }
        }

        public bool RemoveComment(Guid id)
        {
            return RemoveComment(GetCommentByID(id));
        }
    }
}
