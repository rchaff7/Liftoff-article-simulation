using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace liftoff_storefront.Controllers
{
    public class HelloController : Controller
    {
        public IActionResult Index()
        {
            return Content("HELLO WORLD");
        }

        public IActionResult Goodbye()
        {
            return Content("GOODBYE WORLD");
        }
    }
}
