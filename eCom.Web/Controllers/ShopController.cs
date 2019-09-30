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
        ProductService PServices = new ProductService();

        public ActionResult Checkout()
        {
            CheckoutViewModel checkoutModel = new CheckoutViewModel();

            var cartProductsCookie = Request.Cookies["cartProduct"];

            if(cartProductsCookie != null)
            {
                checkoutModel.CartProductIds = cartProductsCookie.Value.Split('-').Select(int.Parse).ToList(); //total numbers

                checkoutModel.CartProducts = PServices.GetProducts(checkoutModel.CartProductIds); //products with the same ids (distinct)
            }

            return View(checkoutModel);
        }
    }
}