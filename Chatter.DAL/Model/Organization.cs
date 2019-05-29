using Chatter.DAL.Model.Base;
using Chatter.DAL.Model.ManyToManyTables;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Chatter.DAL.Model
{
    public class Organization : GroupEntityBase
    {
        public ICollection<Group> Groups { get; set; } = new ObservableCollection<Group>();

        public ICollection<Admin_Organization> Admins { get; set; } = new ObservableCollection<Admin_Organization>();

        public ICollection<User_Organization> Users { get; set; } = new ObservableCollection<User_Organization>();
    }
}
