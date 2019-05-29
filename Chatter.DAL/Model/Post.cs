using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Chatter.DAL.Model.Base;

namespace Chatter.DAL.Model
{
    public class Post : PostEntityBase
    {
        public string PostHeader { get; set; }

        public ICollection<Comment> Comments { get; set; } = new ObservableCollection<Comment>();
    }
}
