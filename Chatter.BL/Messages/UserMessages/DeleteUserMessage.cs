using System;
using System.Collections.Generic;
using System.Text;

namespace Chatter.BL.Messages.UserMessages
{
    public class DeleteUserMessage
    {
        public Guid Id { get; set; }

        public DeleteUserMessage (Guid ID)
        {
            this.Id = Id;
        }
    }
}
