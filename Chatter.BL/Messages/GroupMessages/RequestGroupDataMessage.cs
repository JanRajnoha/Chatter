using Chatter.Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chatter.BL.Messages.GroupMessages
{
    public class RequestGroupDataMessage
    {
        public Guid Id { get; set; }

        public RequestGroupDataType RequestGroupDataType { get; set; }

        public string Sender { get; set; }
    }
}
