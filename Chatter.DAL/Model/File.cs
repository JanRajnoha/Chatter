using Chatter.DAL.Model.Base;
using Chatter.Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chatter.DAL.Model
{
    public class File : EntityBase
    {
        public string Name { get; set; }

        public string Path { get; set; }

        public int Size { get; set; }

        public User UploadedBy { get; set; }

        public Post Post { get; set; }

        public Comment Comment { get; set; }

        public FileType Type { get; set; }

        public DateTime DateTime { get; set; } = DateTime.Now;
    }
}
