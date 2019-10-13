using eCom.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eCom.Web.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace eCom.Web.Controllers
{
    public class OrderController : Controller
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

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult OrderTable(string userId, string status, int? pageNo)
        {
            var ordersModel = new OrdersViewModels();

            ordersModel.UserId = userId;
            ordersModel.Status = status;

            pageNo = pageNo.HasValue && pageNo.Value > 0 ? pageNo.Value : 1;
            var pageSize = ConfigurationService.Instance.PageSize();

            ordersModel.Orders = OrderService.Instance.GetOrders(userId, status, pageNo.Value, 3);

            var totalRecords = OrderService.Instance.GetOrdersCount(userId, status);

            ordersModel.Pager = new Pager(totalRecords, pageNo.Value, 3);

            return PartialView(ordersModel);
        }

        public ActionResult Details(int id)
        {
            var detailsModel = new OrderDetailsViewModel();

            detailsModel.Order = OrderService.Instance.GetOrder(id);

            if (detailsModel.Order != null)
            {
                detailsModel.orderUser = UserManager.FindById(detailsModel.Order.UserId);
            }

            detailsModel.AvailableStatuses = new List<string> { "Pending", "In Progress", "Delivered" };

            return View(detailsModel);
        }

        public ActionResult ChangeStatus(string status, int orderId)
        {
            JsonResult json = new JsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            json.Data = new { success = OrderService.Instance.UpdateOrderStatus(status, orderId) };

            return json;
        }
    }
}