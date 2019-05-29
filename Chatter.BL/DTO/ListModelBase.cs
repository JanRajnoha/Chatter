using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Chatter.BL.DTO
{
    public abstract class ListModelBase
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
