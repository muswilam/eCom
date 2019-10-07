using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eCom.Services;
using eCom.Web.ViewModels;
using eCom.Web.Code;

namespace eCom.Web.Controllers
{
    public class ShopController : Controller
    {
        public ActionResult Index(string searchTerm, decimal? minPrice, decimal? maxPrice, int? categoryId, byte? sortBy)
        {
            var shopModel = new ShopViewModel();

            shopModel.FeaturedCategories = CategoriesService.Instance.GetFeaturedCategories();

            shopModel.MaximumPrice = ProductService.Instance.GetMaxPrice();

            shopModel.Products = ProductService.Instance.GetShopProducts(searchTerm, minPrice, maxPrice, categoryId, sortBy);

            shopModel.SortBy = sortBy;

            return View(shopModel);
        }

        public ActionResult FilterProducts(string searchTerm, decimal? minPrice, decimal? maxPrice, int? categoryId, byte? sortBy)
        {
            var filterModel = new FilterProductViewModel();

            filterModel.Products = ProductService.Instance.GetShopProducts(searchTerm, minPrice, maxPrice, categoryId, sortBy);

            return PartialView(filterModel);
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