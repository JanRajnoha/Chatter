using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Chatter.BL.DTO.Organization;
using Chatter.BL.DTO.Post;
using Chatter.BL.DTO.User;
using Chatter.DAL.Model;
using Chatter.DAL.Model.ManyToManyTables;

namespace Chatter.BL.DTO.Group
{
    public class GroupDetailModelDTO : GroupListModelDTO
    {
        public string Description { get; set; }

        public ICollection<PostDetailModelDTO> Posts { get; set; } = new ObservableCollection<PostDetailModelDTO>();

        public ICollection<UserListModelDTO> Admins { get; set; } = new ObservableCollection<UserListModelDTO>();

        public ICollection<UserListModelDTO> Users { get; set; } = new ObservableCollection<UserListModelDTO>();

        public OrganizationListModelDTO Organization { get; set; }
    }
}
