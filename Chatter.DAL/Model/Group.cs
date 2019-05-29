using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Web;
using Chatter.DAL.Model.Base;
using Chatter.DAL.Model.ManyToManyTables;

namespace Chatter.DAL.Model
{
    public class Group : GroupEntityBase
    {
        public ICollection<Post> Posts { get; set; } = new ObservableCollection<Post>();

        public ICollection<Admin_Group> Admins { get; set; } = new ObservableCollection<Admin_Group>();

        public ICollection<User_Group> Users { get; set; } = new ObservableCollection<User_Group>();
    }
}
