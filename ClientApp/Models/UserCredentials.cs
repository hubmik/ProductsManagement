using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.Models
{
    public class UserCredentials
    {
        public static string SessionKey;
        public readonly DateTime CurrentTime = Time();
        
        private static DateTime Time()
        {
            DateTime time;
            using (var client = new WcfService.Service1Client())
            {
                time = client.GetCurrentTime();
            }
            return time;
        }
    }
}
