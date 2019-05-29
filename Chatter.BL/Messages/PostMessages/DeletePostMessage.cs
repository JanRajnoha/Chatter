using System;
using System.Collections.Generic;
using System.Text;

namespace Chatter.BL.Messages.PostMessages
{
    public class DeletePostMessage
    {
        public Guid Id { get; set; }

        public DeletePostMessage(Guid ID)
        {
            this.Id = Id;
        }
    }
}
