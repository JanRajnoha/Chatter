using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Chatter.DAL.Model.ManyToManyTables
{
    public class Admin_Organization
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("UserId")]
        public Guid AdminRefId { get; set; }

        public virtual User Admin { get; set; }

        [ForeignKey("OrganizationId")]
        public Guid OrganizationRefId { get; set; }

        public virtual Organization Organization { get; set; }
    }
}
