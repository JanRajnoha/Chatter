using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Chatter.DAL.Model.Base;

namespace Chatter.DAL.Model.ManyToManyTables
{
    public class User_Group : EntityBase
    {
        [ForeignKey("UserId")]
        public Guid UserRefId { get; set; }
        public virtual User User { get; set; }

        [ForeignKey("GroupId")]
        public Guid GroupRefId { get; set; }

        public virtual Group Group { get; set; }
    }
}
