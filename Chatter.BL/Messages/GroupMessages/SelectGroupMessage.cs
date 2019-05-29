using System;
using System.Collections.Generic;
using System.Text;

namespace Chatter.BL.Messages.GroupMessages
{
    public class SelectGroupMessage
    {
        public Guid Id { get; set; }

        public SelectGroupMessage(Guid Id)
        {
            this.Id = Id;
        }
    }
}
