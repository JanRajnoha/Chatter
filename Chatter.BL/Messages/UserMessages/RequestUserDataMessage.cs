using Chatter.Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chatter.BL.Messages.UserMessages
{
    public class RequestUserDataMessage
    {
        public Guid Id { get; set; }

        public RequestUserDataType RequestUserDataType { get; set; }

        public string Sender { get; set; }
    }
}
