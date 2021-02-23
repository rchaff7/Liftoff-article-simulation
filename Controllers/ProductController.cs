using DeepAI;
using liftoff_storefront.Data;
using liftoff_storefront.Models;
using liftoff_storefront.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace liftoff_storefront.Controllers
{
    public class ProductController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private StorefrontDbContext context;
        private DeepAI_API api = new DeepAI_API(apiKey: "40185d3f-a067-45aa-8885-58767b613184");

        public ProductController(StorefrontDbContext dbcontext, UserManager<IdentityUser> usermanager)
        {
            context = dbcontext;
            userManager = usermanager;
        }

        [HttpGet("/product")]
        public IActionResult GenerateProduct()
        {
            return View();
        }

        [HttpPost("/product")]
        public IActionResult GenerateProduct(Product product)
        {
            Product newProduct = new Product(product.Name, null);
            context.Products.Add(newProduct);
            context.SaveChanges();
            newProduct.ImageURL = "/image/" + newProduct.Id + ".jpg";
            context.Update(newProduct);
            context.SaveChanges();

            WebClient webClient = new WebClient();
            StandardApiResponse resp = api.callStandardApi("text2img", new { text = product.Name });
            webClient.DownloadFile(resp.output_url, "wwwroot/Image/" + newProduct.Id + ".jpg");
            
            return Redirect("/product/"+newProduct.Id);
        }

        public IActionResult Index()
        {
            List<Product> products = context.Products.ToList();
            return View(products);
        }

        [HttpGet("/product/{id}")]
        public IActionResult ProductPage(int id)
        {
            Product product = context.Products.Find(id);
            ViewBag.desc = new HtmlString(product.Description);
            return View(product);
        }

        [HttpGet("/product/{id}/comments")]
        public IActionResult ViewComments(int id)
        {
            List<UserComment> comments = context.UserComments
                .Where(x => x.ProductId == id)
                .Include(x => x.Product)
                .Include(x => x.IdentityUser)
                .ToList();
            ViewBag.id = id;
            ViewBag.uid = userManager.GetUserId(User);
            return View(comments);
        }

        [Authorize]
        [HttpGet("/product/{id}/comments/add")]
        public IActionResult AddComment(int id)
        {
            AddCommentViewModel viewmodel = new AddCommentViewModel { ProductId = id };
            return View(viewmodel);
        }

        [Authorize]
        [HttpPost("/product/{id}/comments/add")]
        public IActionResult AddComment(AddCommentViewModel viewmodel)
        {
            if (ModelState.IsValid)
            {
                UserComment comment = new UserComment();
                comment.Content = viewmodel.Content;
                comment.ProductId = viewmodel.ProductId;
                comment.IdentityUserId = userManager.GetUserId(User);

                context.UserComments.Add(comment);
                context.SaveChanges();
                return Redirect("/home");
            }
            return View("AddComment", viewmodel);
        }
    }
}
