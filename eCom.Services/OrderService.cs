using eCom.Data;
using eCom.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCom.Services
{
    public class OrderService
    {
        #region Singleton Non-Thread Safety
        public static OrderService Instance
        {
            get
            {
                if (instance == null) instance = new OrderService();

                return instance;
            }
        }
        private static OrderService instance { get; set; }
        private OrderService()
        {
        }
        #endregion

        //get list of orders by userId, status, pageNo
        public List<Order> GetOrders(string userId, string status, int pageNo, int pageSize)
        {
            using (var context = new eComContext())
            {
                IQueryable<Order> orders = context.Orders.OrderBy(p => p.Id);

                if (!string.IsNullOrEmpty(userId))
                    orders = orders.Where(p => p.UserId.ToLower().Contains(userId.ToLower()));

                if (!string.IsNullOrEmpty(status))
                    orders = orders.Where(p => p.Status.ToLower().Contains(status.ToLower()));

                return orders.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        //get count of list of orders
        public int GetOrdersCount(string userId, string status)
        {
            using (var context = new eComContext())
            {
                var orders = context.Orders.ToList();

                if (!string.IsNullOrEmpty(userId))
                    orders = orders.Where(p => p.UserId.ToLower().Contains(userId.ToLower())).ToList();

                if (!string.IsNullOrEmpty(status))
                    orders = orders.Where(p => p.Status.ToLower().Contains(status.ToLower())).ToList();

                return orders.Count();
            }
        }

        //create order with creating list of orderItems { orderId , productId }
        public int SaveOrder(Order order)
        {
            using (var context = new eComContext())
            {
                context.Orders.Add(order);
                return context.SaveChanges();
            }
        }

    }
}
