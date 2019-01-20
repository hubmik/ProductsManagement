using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerApp.Models
{
    public static class SessionProcess
    {
        public static int SessionIdentifier = System.Web.HttpContext.Current.User.Identity.GetUserId<int>();
        public static bool IsSessionDisposed { get; set; }
    }
}