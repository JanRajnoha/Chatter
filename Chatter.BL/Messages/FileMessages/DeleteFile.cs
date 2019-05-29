using System;
using System.Collections.Generic;
using System.Text;

namespace Chatter.BL.Messages.FileMessages
{
    public class DeleteFileMessage
    {
        public Guid Id { get; set; }

        public DeleteFileMessage(Guid ID)
        {
            this.Id = Id;
        }
    }
}

