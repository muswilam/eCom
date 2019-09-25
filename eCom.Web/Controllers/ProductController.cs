using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eCom.Services;
using eCom.Entities;

namespace eCom.Web.Controllers
{
    public class ProductController : Controller
    {
        ProductService PServices = new ProductService();

        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult ProductTable(string search)
        {
            var products = PServices.GetProducts();

            if(!string.IsNullOrEmpty(search))
                products = products.Where(p => p.Name.ToLower().Contains(search.ToLower())).ToList();
            
            return PartialView(products);
        }

        [HttpGet]
        public PartialViewResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            PServices.SaveProduct(product);

            return RedirectToAction("ProductTable");
        }
    }
}