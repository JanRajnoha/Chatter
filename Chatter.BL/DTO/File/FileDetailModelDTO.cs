using Chatter.BL.DTO.Comment;
using Chatter.BL.DTO.Post;
using Chatter.BL.DTO.User;
using Chatter.Common.Enum;
using System;

namespace Chatter.BL.DTO.File
{
    public class FileDetailModelDTO : FileListModelDTO
    {
        public string Path { get; set; }

        public UserDetailModelDTO UploadedBy { get; set; }

        public PostDetailModelDTO Post { get; set; }

        public CommentDetailModelDTO Comment { get; set; }

        public FileType FileType { get; set; }

        public DateTime DateTime { get; set; } = DateTime.Now;
    }
}
