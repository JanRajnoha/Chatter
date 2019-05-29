using Chatter.BL.DTO.Group;
using Chatter.BL.DTO.User;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Chatter.BL.DTO.Organization
{
    public class OrganizationDetailModelDTO : OrganizationListModelDTO
    {
        public string Description { get; set; } = string.Empty;

        public ICollection<GroupDetailModelDTO> Groups { get; set; } = new ObservableCollection<GroupDetailModelDTO>();

        public ICollection<UserListModelDTO> Admins { get; set; } = new ObservableCollection<UserListModelDTO>();

        public ICollection<UserListModelDTO> Users { get; set; } = new ObservableCollection<UserListModelDTO>();
    }
}
