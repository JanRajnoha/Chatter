using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Chatter.Classes
{
    public class Functions
    {
        public static bool CheckConnection()
        {
            WebClient client = new WebClient();
            try
            {
                using (client.OpenRead("http://www.google.com"))
                {
                }
                return true;
            }
            catch (WebException)
            {
                return false;
            }
        }
    }
}
