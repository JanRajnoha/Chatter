using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Chatter.BL.DTO.Comment;
using Chatter.BL.DTO.File;
using Chatter.BL.DTO.Group;
using Chatter.BL.DTO.User;

namespace Chatter.BL.DTO.Post
{
    public class PostDetailModelDTO : PostListModelDTO
    {
        public string Content { get; set; } = string.Empty;

        public UserDetailModelDTO User { get; set; }

        public ICollection<CommentDetailModelDTO> Comments { get; set; } = new ObservableCollection<CommentDetailModelDTO>();
    }
}
