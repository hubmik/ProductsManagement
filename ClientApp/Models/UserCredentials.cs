using CustomerApp.Models;
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

        public bool IsUserAuthenticated(string accessKey)
        {
            string key = accessKey;
            List<string> list = null;

            using (var context = new ApplicationDbContext())
            {
                IQueryable<string> keysList = context.Employees.Select(x => x.AccessKey);
                list = keysList.ToList();
            }
            if (list.Any(key.Contains))
                return true;

            return false;
        }
    }
}
