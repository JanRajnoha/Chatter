using System;
using System.Collections.Generic;
using System.Text;

namespace Chatter.BL.Messages.PostMessages
{
    public class SelectPostMessage
    {
        public Guid Id { get; set; }

        public SelectPostMessage(Guid Id)
        {
            this.Id = Id;
        }
    }
}
