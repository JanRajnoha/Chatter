using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatter.Classes
{
    public class EditUserHelpClass
    {
        public Guid Id { get; set; }
        public string OldPass { get; set; } = string.Empty;
        public string NewPass { get; set; } = string.Empty;
        public string NewPassAgain { get; set; } = string.Empty;
    }
}
