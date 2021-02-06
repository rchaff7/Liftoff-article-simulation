using liftoff_storefront.Models;
using liftoff_storefront.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using Microsoft.AspNetCore.Html;

namespace liftoff_storefront.Controllers
{
    public class HomeController : Controller
    {
        private StorefrontDbContext context;
        public HomeController(StorefrontDbContext dbcontext)
        {
            context = dbcontext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet("/product/{id}")]
        public IActionResult ProductPage(int id)
        {
            Product product = context.Products.Find(id);
            ViewBag.desc = new HtmlString(product.Description);
            return View(product);
        }


        //public IActionResult ViewComments()
        //{
        //    return View();
        //}



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
