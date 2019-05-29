using Chatter.DAL.Model.ManyToManyTables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Chatter.DAL.Model.Base
{
    public abstract class GroupEntityBase : EntityBase
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
