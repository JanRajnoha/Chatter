using Chatter.BL.DTO.File;
using Chatter.BL.DTO.Group;
using Chatter.BL.DTO.Post;
using Chatter.BL.DTO.Tag;
using Chatter.BL.DTO.User;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Chatter.BL.DTO.Comment
{
    public class CommentDetailModelDTO : CommentListModelDTO
    {
        public UserDetailModelDTO User { get; set; }

        public ObservableCollection<TagDetailModelDTO> Tags { get; set; } = new ObservableCollection<TagDetailModelDTO>();

        public PostListModelDTO Post { get; set; }
    }
}
