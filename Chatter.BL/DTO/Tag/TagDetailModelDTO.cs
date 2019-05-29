using Chatter.BL.DTO.Comment;
using Chatter.BL.DTO.Post;
using Chatter.BL.DTO.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chatter.BL.DTO.Tag
{
    public class TagDetailModelDTO: ListModelBase
    {
        public PostDetailModelDTO Post { get; set; }

        public CommentDetailModelDTO Comment { get; set; }

        public UserDetailModelDTO User { get; set; }
    }
}
