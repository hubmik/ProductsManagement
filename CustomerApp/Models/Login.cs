using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerApp.Models
{
    public class Login : Model.Users
    {
        public List<Model.Users> GetUsers()
        {
            List<Model.Users> users;
            using (var context = new Model.Db())
            {
                users = context.Users.ToList();
            }

            return users;
        }
    }
}