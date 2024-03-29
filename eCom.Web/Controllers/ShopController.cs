﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eCom.Services;
using eCom.Web.ViewModels;
using eCom.Web.Code;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using eCom.Entities;
using eCom.Web.Models;

namespace eCom.Web.Controllers
{
    public class ShopController : Controller
    {
        #region User Manager
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        #endregion

        public ActionResult Index(int? categoryId) 
        {
            var shopModel = new ShopViewModel();

            shopModel.FilledCategories = CategoriesService.Instance.GetFilledCategories();

            shopModel.MaximumPrice = ProductService.Instance.GetMaxPrice();
            shopModel.CategoryId = categoryId;

            return View(shopModel);
        }

        public ActionResult FilterProducts(string searchTerm, decimal? minPrice, decimal? maxPrice, int? categoryId, byte? sortBy, int? pageNo)
        {
            var filterModel = new FilterProductViewModel();

            pageNo = pageNo.HasValue && pageNo.Value > 0 ? pageNo.Value : 1;
            var pageSize = ConfigurationService.Instance.ShopPageSize(); //6

            filterModel.SortBy = sortBy;
            filterModel.CategoryId = categoryId;
            filterModel.SearchTerm = searchTerm;

            filterModel.FilledCategories = CategoriesService.Instance.GetFilledCategories();

            filterModel.MaximumPrice = ProductService.Instance.GetMaxPrice();
            int totalRecords = ProductService.Instance.GetShopProductsCount(searchTerm, minPrice, maxPrice, categoryId, sortBy);

            filterModel.Products = ProductService.Instance.GetShopProducts(searchTerm, minPrice, maxPrice, categoryId, sortBy, pageNo.Value, pageSize);

            filterModel.Pager = new Pager(totalRecords, pageNo, pageSize);
            filterModel.User = UserManager.FindById(User.Identity.GetUserId());

            return PartialView(filterModel);
        }

        [Authorize]
        public ActionResult Checkout()
        {
            return View();
        }

        public PartialViewResult CheckoutPartial()
        {
            var checkoutModel = new CheckoutViewModel();

            var user = UserManager.FindById(User.Identity.GetUserId());

            var cartProductsCookie = Request.Cookies[user.Name.Replace(" ","") + "CartProducts"];

            if (cartProductsCookie != null && !string.IsNullOrEmpty(cartProductsCookie.Value))
            {
                var userName = cartProductsCookie.Value.Split('-').First().Replace("%40","@");
                if (userName == User.Identity.Name)
                {
                    checkoutModel.CartProductIds = cartProductsCookie.Value.Split('-').Skip(1).Select(int.Parse).ToList(); //total numbers

                    checkoutModel.CartProducts = ProductService.Instance.GetProducts(checkoutModel.CartProductIds); //products with the same ids (distinct)

                    checkoutModel.User = user;
                }
            }

            return PartialView(checkoutModel);
        }

        public JsonResult PlaceOrder(string productIds)
        {
            var json = new JsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            if (!string.IsNullOrEmpty(productIds))
            {
                var splitedProductIds = productIds.Split(new[] { '-' }).Select(int.Parse).ToList(); //[7,7,10,1]

                var boughtProducts = ProductService.Instance.GetProducts(splitedProductIds.Distinct().ToList()); //[7,10,1]

                Order newOrder = new Order();

                newOrder.UserId = User.Identity.GetUserId();
                newOrder.OrderedAt = DateTime.Now;
                newOrder.Status = "Pending";

                //Iterate over all splitedIds matched with distinct ids, counted ,then * of unit price then sum all to get total
                newOrder.TotalAmount = boughtProducts.Sum(bP => bP.Price * splitedProductIds.Where(pId => pId == bP.Id).Count());

                newOrder.OrderItems = new List<OrderItem>();
                newOrder.OrderItems.AddRange(boughtProducts.Select(bP => new OrderItem() { ProductId = bP.Id, Quantity = splitedProductIds.Where(pId => pId == bP.Id).Count() }));

                var rowsEffeced = OrderService.Instance.SaveOrder(newOrder);

                json.Data = new { success = true, rows = rowsEffeced };
            }
            else
            {
                 json.Data = new { success = false }; 
            }

            return json;
        }

        [HttpPost]
        public ActionResult UpdateUser(UserViewModel userModel)
        {
            if(ModelState.IsValid)
            {
                var userInDb = UserManager.FindById(User.Identity.GetUserId());
                userInDb.Name = userModel.Name;
                userInDb.Address = userModel.Address;
                userInDb.PhoneNumber = "(+20) " + userModel.PhoneNumber;
                IdentityResult result = UserManager.Update(userInDb);

                if (result.Succeeded)
                {
                    return RedirectToAction("CheckoutPartial");
                }
            }

            return new HttpStatusCodeResult(500);
        }

        [Authorize]
        public ActionResult OrderPlaced()
        {
            return View();
        }

        public ActionResult _OrderPlaced(int? pageNo)
        {
            OrderPlacedViewModel orderPlacedModel = new OrderPlacedViewModel();

            pageNo = pageNo.HasValue && pageNo.Value > 0 ? pageNo.Value : 1;
            var pageSize = ConfigurationService.Instance.ShopPageSize(); //6

            int totalRecords = OrderService.Instance.GetOrdersCountByUserId(User.Identity.GetUserId());

            orderPlacedModel.User = UserManager.FindById(User.Identity.GetUserId());

            orderPlacedModel.Orders = OrderService.Instance.GetOrdersByUserId(User.Identity.GetUserId(), pageNo.Value, pageSize);

            orderPlacedModel.Pager = new Pager(totalRecords, pageNo, pageSize);

            return PartialView(orderPlacedModel);
        }
    }
}