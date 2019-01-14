using CustomerApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.Models
{
    public class UserIdentification
    {
        public async Task<Employees> GetUserIdentity()
        {
            Employees employee = null;

            using (var client = new WcfService.Service1Client())
            {
                employee = await client.GetUserCredentialsAsync(UserCredentials.SessionKey);
            }

            return employee;
        }
    }
}
