using System;
using System.Collections.Generic;
using System.Text;
using Chatter.BL.DTO.Group;

namespace Chatter.BL.Messages.GroupMessages
{
    public class UpdateGroupMessage
    {
        public GroupDetailModelDTO UpdatedGroup { get; set; }
    }
}
