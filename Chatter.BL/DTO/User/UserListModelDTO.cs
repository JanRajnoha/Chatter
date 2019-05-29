using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Chatter.BL.DTO.User
{
    public class UserListModelDTO : ListModelBase
    {
        [Required, MaxLength(20)]
        public string Username { get; set; } = string.Empty;

        [Required, MinLength(2), MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MinLength(2), MaxLength(50)]
        public string LastName { get; set; }
    }
}
