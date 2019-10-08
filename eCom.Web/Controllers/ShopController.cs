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
        public ActionResult Index(string searchTerm, decimal? minPrice, decimal? maxPrice, int? categoryId, byte? sortBy, int? pageNo)
        {
            var shopModel = new ShopViewModel();

            int totalRecords = ProductService.Instance.GetShopProductsCount(searchTerm, minPrice, maxPrice, categoryId, sortBy);
            pageNo = pageNo.HasValue && pageNo.Value > 0 ? pageNo.Value : 1;

            shopModel.FeaturedCategories = CategoriesService.Instance.GetFeaturedCategories();

            shopModel.MaximumPrice = ProductService.Instance.GetMaxPrice();

            shopModel.Products = ProductService.Instance.GetShopProducts(searchTerm, minPrice, maxPrice, categoryId, sortBy, pageNo.Value, 9);

            shopModel.SortBy = sortBy;
            shopModel.CategoryId = categoryId;

            shopModel.Pager = new Pager(totalRecords, pageNo, 9);

            return View(shopModel);
        }

        public ActionResult FilterProducts(string searchTerm, decimal? minPrice, decimal? maxPrice, int? categoryId, byte? sortBy, int? pageNo)
        {
            var filterModel = new FilterProductViewModel();

            int totalRecords = ProductService.Instance.GetShopProductsCount(searchTerm, minPrice, maxPrice, categoryId, sortBy);
            pageNo = pageNo.HasValue && pageNo.Value > 0 ? pageNo.Value : 1;

            filterModel.Products = ProductService.Instance.GetShopProducts(searchTerm, minPrice, maxPrice, categoryId, sortBy, pageNo.Value, 9);

            filterModel.Pager = new Pager(totalRecords, pageNo, 9);

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