using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Chatter.DAL.Model.Base;

namespace Chatter.DAL.Model.ManyToManyTables
{
    public class User_Organization : EntityBase
    {
        [ForeignKey("UserId")]
        public Guid UserRefId { get; set; }

        public virtual User User { get; set; }

        [ForeignKey("OrganizationId")]
        public Guid OrganizationRefId { get; set; }

        public virtual Organization Organization { get; set; }
    }
}
