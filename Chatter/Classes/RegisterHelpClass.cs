using Chatter.BL.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatter.Classes
{
    public class RegisterHelpClass
    {
        public UserDetailModelDTO User { get; set; } = new UserDetailModelDTO();

        public string PassAgain { get; set; } = "";
    }
}
