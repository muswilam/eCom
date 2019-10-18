using eCom.Entities;
using eCom.Services;
using eCom.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace eCom.Web.Controllers
{
    public class ReviewController : Controller
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

        public ActionResult AddReview(AddReviewViewModel addReviewModel)
        {
            
            bool result = false;

            var newReview = new Review();
            newReview.UserId = addReviewModel.UserId;
            newReview.Body = addReviewModel.Body;
            newReview.ReviewedAt = DateTime.Now;
            newReview.Rate = 0;
            newReview.ProductId = addReviewModel.ProductId;

            result = ReviewService.Instance.SaveReview(newReview);

            if (result)
                return RedirectToAction("_Reviews", new { productId = newReview.ProductId});

            return new HttpStatusCodeResult(500);
        }

        public PartialViewResult _Reviews(int productId)
        {
            var reviewsModel = new ReviewsViewModel();

            reviewsModel.Reviews = ReviewService.Instance.GetReviews(productId);
            reviewsModel.AuthenticatedUser = UserManager.FindById(User.Identity.GetUserId());
            reviewsModel.User = UserManager;

            reviewsModel.ProductId = productId;

            return PartialView(reviewsModel);
        }
    }
}