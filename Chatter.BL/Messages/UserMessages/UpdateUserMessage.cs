using System;
using System.Collections.Generic;
using System.Text;
using Chatter.BL.DTO.User;

namespace Chatter.BL.Messages.UserMessages
{
    public class UpdateUserMessage
    {
        public UserDetailModelDTO UpdateUser { get; set; }
    }
}