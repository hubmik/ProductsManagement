using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerApp.ViewModels
{
    public class LoginViewModel
    {
        public Users GetUserCredentials()
        {
            Users user = null;
            using (var client = new WcfService.Service1Client())
            {

            }
            Db context = new Db();
            //IQueryable<Users> userCred = context.Users.

            return user;
        }
    }
}