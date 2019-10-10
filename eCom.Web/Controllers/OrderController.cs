using eCom.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eCom.Web.ViewModels;

namespace eCom.Web.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index(string userId, string status, int? pageNo)
        {
            var orderModel = new OrdersViewModels();

            orderModel.UserId = userId;
            orderModel.Status = status;

            pageNo = pageNo.HasValue && pageNo.Value > 0 ? pageNo.Value : 1;
            var pageSize = ConfigurationService.Instance.PageSize();

            orderModel.Orders = OrderService.Instance.GetOrders(userId, status, pageNo.Value, 3);

            var totalRecords = OrderService.Instance.GetOrdersCount(userId, status);

            orderModel.Pager = new Pager(totalRecords, pageNo.Value, 3);

            return View(orderModel);
        }
    }
}