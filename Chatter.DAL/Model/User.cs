using System;
using System.Collections.Generic;
using Chatter.DAL.Model.Base;
using Chatter.DAL.Model.ManyToManyTables;
using System.Collections.ObjectModel;
using Chatter.Common.Enum;

namespace Chatter.DAL.Model
{
    public class User : EntityBase
    {
        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Gender Gender { get; set; }

        public string Description { get; set; }

        public string Email { get; set; }

        public byte[] Password { get; set; }

        public byte[] Salt { get; set; }

        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        public ICollection<User_Group> Groups { get; set; } = new ObservableCollection<User_Group>();

        public ICollection<User_Organization> Organizations { get; set; } = new ObservableCollection<User_Organization>();

        public ICollection<SignInLog> SignInLogs { get; set; } = new ObservableCollection<SignInLog>();

        public ICollection<Admin_Group> AdminGroups { get; set; } = new ObservableCollection<Admin_Group>();

        public ICollection<Admin_Organization> AdminOrganizations { get; set; } = new ObservableCollection<Admin_Organization>();
    }
}