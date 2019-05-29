using System;
using System.Collections.Generic;
using System.Text;
using Chatter.BL.DTO.Comment;
using Chatter.BL.DTO.Post;

namespace Chatter.BL.DBAccess.Interfaces
{
    public interface IDBPost
    {
        PostDetailModelDTO AddPost(PostDetailModelDTO post);
        PostDetailModelDTO GetPostById(Guid Id);
        PostDetailModelDTO GetPostByComment(CommentDetailModelDTO comment);
        List<PostDetailModelDTO> GetPostDetailList(Guid groupId);
        List<PostDetailModelDTO> GetPostList();
        void RemovePost(Guid Id);
        PostDetailModelDTO FindPostById(Guid Id);
        void Update(PostDetailModelDTO post);
    }
}
