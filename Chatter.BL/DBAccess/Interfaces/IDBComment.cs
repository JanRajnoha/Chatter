using Chatter.BL.DTO.Comment;
using Chatter.BL.DTO.Post;
using Chatter.BL.DTO.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chatter.BL.DBAccess.Interfaces
{
    public interface IDBComment
    {
        CommentDetailModelDTO AddComment(CommentDetailModelDTO comment);

        bool RemoveComment(CommentDetailModelDTO comment);

        bool RemoveComment(Guid id);

        bool EditComment(CommentDetailModelDTO comment);

        CommentDetailModelDTO GetCommentByID(Guid id);

        ICollection<CommentDetailModelDTO> GetCommentsByPostID(Guid postId);

        ICollection<CommentDetailModelDTO> GetCommentsByUser(UserDetailModelDTO user);

        ICollection<CommentDetailModelDTO> GetAllComments();
    }
}
