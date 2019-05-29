using Chatter.BL.DTO.Group;
using Chatter.BL.DTO.LogIn;
using Chatter.BL.DTO.Organization;
using Chatter.Common.Enum;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Chatter.BL.DTO.User
{
    public class UserDetailModelDTO : UserListModelDTO
    {
        [Required]
        public Gender Gender { get; set; } = Gender.Male;

        [StringLength(200)]
        public string Description { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; } = "";

        [Required, MinLength(6), PasswordPropertyText]
        public string Password { get; set; } = string.Empty;

        public byte[] Hash { get; set; }

        public byte[] Salt { get; set; }

        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        public ICollection<GroupListModelDTO> Groups { get; set; } = new ObservableCollection<GroupListModelDTO>();

        public ICollection<OrganizationListModelDTO> Organizations { get; set; } = new ObservableCollection<OrganizationListModelDTO>();

        public ICollection<LogInDetailModelDTO> SignInLogs { get; set; } = new ObservableCollection<LogInDetailModelDTO>();

        public ICollection<GroupListModelDTO> AdminGroups { get; set; } = new ObservableCollection<GroupListModelDTO>();

        public ICollection<OrganizationListModelDTO> AdminOrganizations { get; set; } = new ObservableCollection<OrganizationListModelDTO>();

    }
}
