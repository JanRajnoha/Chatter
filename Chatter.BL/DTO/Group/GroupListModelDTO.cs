using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Chatter.BL.DTO.Group
{
    public class GroupListModelDTO : ListModelBase
    {
        [Required]
        public string Name { get; set; }
    }
}
