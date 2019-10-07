using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eCom.Services;
using eCom.Web.ViewModels;

namespace eCom.Web.Controllers
{
    public class ShopController : Controller
    {
        public ActionResult Index(string searchTerm, int? minPrice , int? maxPrice, int? categoryId)
        {
            var shopModel = new ShopViewModel();

            shopModel.FeaturedCategories = CategoriesService.Instance.GetFeaturedCategories();

            shopModel.MaximumPrice = ProductService.Instance.GetMaxPrice();

            shopModel.Products = ProductService.Instance.GetShopProducts(searchTerm, minPrice, maxPrice, categoryId);

            return View(shopModel);
        }

        public ActionResult Checkout()
        {
            CheckoutViewModel checkoutModel = new CheckoutViewModel();

            var cartProductsCookie = Request.Cookies["cartProduct"];

            if(cartProductsCookie != null)
            {
                checkoutModel.CartProductIds = cartProductsCookie.Value.Split('-').Select(int.Parse).ToList(); //total numbers

                checkoutModel.CartProducts = ProductService.Instance.GetProducts(checkoutModel.CartProductIds); //products with the same ids (distinct)
            }

            return View(checkoutModel);
        }
    }
}