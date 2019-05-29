using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Chatter.DAL.Model.ManyToManyTables
{
    public class Admin_Group
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("AdminId")]
        public Guid AdminRefId { get; set; }

        public virtual User Admin { get; set; }

        [ForeignKey("GroupId")]
        public Guid GroupRefId { get; set; }

        public virtual Group Group { get; set; }
    }
}
