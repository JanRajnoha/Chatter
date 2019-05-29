using Chatter.Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chatter.BL.Messages.LogMessages
{
    public class SignInApprovedMessage
    {
        public SignInType SignInType { get; set; }

        public Guid UserId { get; set; }
    }
}