using eCom.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eCom.Web.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using eCom.Web.Code;

namespace eCom.Web.Controllers
{
    [Authorize(Roles = "Admin")]
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

        public ActionResult OrderTable( string status, string date, int? pageNo)
        {
            var ordersModel = new OrdersViewModels();

            ordersModel.Status = status;
            ordersModel.SelectedDate = date;

            pageNo = pageNo.HasValue && pageNo.Value > 0 ? pageNo.Value : 1;
            var pageSize = ConfigurationService.Instance.PageSize();

            ordersModel.Orders = OrderService.Instance.GetOrders(status, pageNo.Value, date, pageSize);

            var totalRecords = OrderService.Instance.GetOrdersCount(status, date);

            ordersModel.Pager = new Pager(totalRecords, pageNo.Value, pageSize);
            ordersModel.AvailableStatus = new List<string>() { "Pending", "In Progress", "Delivered" };

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

            detailsModel.AvailableStatuses = new List<string> { "Pending", "In Progress" , "Delivered"};

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