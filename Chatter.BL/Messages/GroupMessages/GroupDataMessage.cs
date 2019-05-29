using Chatter.BL.DTO.Group;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chatter.BL.Messages.GroupMessages
{
    public class GroupDataMessage
    {
        public GroupDetailModelDTO GroupData { get; set; }

        public string Receiver { get; set; }
    }
}
