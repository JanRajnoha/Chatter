using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Chatter.DAL.Model.Base
{
    public abstract class PostEntityBase : EntityBase
    {
        public string Content { get; set; }

        public DateTime DateTime { get; set; } = DateTime.Now;

        public User User { get; set; }
    }
}
