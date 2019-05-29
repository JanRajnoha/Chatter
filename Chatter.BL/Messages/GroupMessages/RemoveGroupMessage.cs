using Chatter.BL.DTO.Group;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chatter.BL.Messages.GroupMessages
{
    public class RemoveGroupMessage
    {
        public GroupDetailModelDTO SelectedGroup { get; set; }
    }
}
