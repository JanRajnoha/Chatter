using Chatter.BL.DTO.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chatter.BL.Messages.UserMessages
{
    public class UserDataMessage
    {
        public UserDetailModelDTO UserData { get; set; }

        public string Receiver { get; set; }
    }
}
