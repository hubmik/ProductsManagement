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
        public Employees GetUserCredentials(string accessKey)
        {
            Employees employee = null;
            using (var context = new ApplicationDbContext())
            {
                var query = context.Employees
                    .FirstOrDefault(x => x.AccessKey == accessKey);

                employee = query as Employees;
            }

            return employee;
        }
    }
}
