using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CustomerApp.Controllers
{
    public class OrderController : AsyncController
    {
        public async Task<ViewResult> Orders()
        {
            return View(nameof(Orders));
        }
    }
}