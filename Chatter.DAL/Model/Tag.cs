using Chatter.DAL.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chatter.DAL.Model
{
    public class Tag : EntityBase
    {
        public Post Post { get; set; }

        public Comment Comment { get; set; }

        public User User { get; set; }
    }
}
