using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Chatter.DAL.Model.Base;

namespace Chatter.DAL.Model
{
    public class Comment : PostEntityBase
    {
        public Post Post { get; set; }
    }
}
