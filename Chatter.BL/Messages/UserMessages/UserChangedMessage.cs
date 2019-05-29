using Chatter.Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chatter.BL.Messages.UserMessages
{
    public class UserChangedMessage
    {
        public Guid Id { get; set; }

        public UserChangedType UserChangedType { get; set; } = UserChangedType.Profile;
    }
}
