using System;
using System.Collections.Generic;
using System.Text;

namespace Chatter.BL.Messages.GroupMessages
{
    public class DeleteGroupMessage
    {
        public Guid Id { get; set; }

        public DeleteGroupMessage(Guid ID)
        {
            this.Id = Id;
        }
    }
}
