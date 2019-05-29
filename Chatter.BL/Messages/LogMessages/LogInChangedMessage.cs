using Chatter.Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chatter.BL.Messages.LogMessages
{
    public class LogInChangedMessage
    {
        public Guid UserId { get; set; }

        public SignLogCode SignLogCode { get; set; }
    }
}
