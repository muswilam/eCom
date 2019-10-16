using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using eCom.Entities;
using eCom.Services;
using eCom.Web.ViewModels;

namespace eCom.Web.Controllers
{
    public class WishlistController : Controller
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

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult WishlistItems()
        {
            WishlistViewModel wishModel = new WishlistViewModel();

            wishModel.User = UserManager.FindById(User.Identity.GetUserId());

            wishModel.Wishlist = WishlistService.Instance.GetWishlist(User.Identity.GetUserId());

            return PartialView(wishModel);
        }


        public JsonResult AddWishlist(int productId)
        {
            JsonResult json = new JsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            bool result = false;

            json.Data = new { success = false, message = "Something went wrong." };

            var userId = User.Identity.GetUserId();

            if (WishlistService.Instance.IsExistWishlist(userId)) //add items to existing wishlist (userId) 
            {
                var existWishlist = WishlistService.Instance.GetWishlist(userId);

                if (WishlistService.Instance.IsExistProduct(productId,existWishlist))
                {
                    json.Data = new { success = false, message = "Product added to wishlist before" };
                }
                else
                {
                    existWishlist.WishlistItems.Clear(); //so important : to prevent overriding old item that added before.
                    existWishlist.WishlistItems.Add(new WishlistItem() { ProductId = productId, WishlistId = existWishlist.Id });

                    result = WishlistService.Instance.AddWishlistItem(existWishlist.WishlistItems);
                }
            }
            else //create new wishlist
            {
                Wishlist newWishlist = new Wishlist();

                newWishlist.UserId = userId;
                newWishlist.WishlistItems = new List<WishlistItem>();
                newWishlist.WishlistItems.Add(new WishlistItem() { ProductId = productId });

                result = WishlistService.Instance.SaveWishlist(newWishlist);

            }

            if (result)
                json.Data = new { success = true };

            return json;
        }

        public JsonResult RemoveProduct(int productId)
        {
            JsonResult json = new JsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            bool result = false;

            var userId = User.Identity.GetUserId();
 
            result = WishlistService.Instance.RemoveItemFromWishlist(userId, productId);

            if (result)
                json.Data = new { success = true };
            else
                json.Data = new { success = false, message = "Something went wrong." };

            return json;
        }

        public ActionResult EmptyWishlist()
        {
            JsonResult json = new JsonResult(); ;
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            bool result = false;

            var userId = User.Identity.GetUserId();

            result = WishlistService.Instance.EmptyWishlistItems(userId);

            if (result)
                json.Data = new { success = true };
            else
                json.Data = new { success = false, message = "Something went wrong." };

            return json;
        }
    }
}