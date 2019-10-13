using eCom.Data;
using eCom.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

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

        //get list of orders by status, pageNo
        public List<Order> GetOrders(string status, int pageNo, int pageSize)
        {
            using (var context = new eComContext())
            {
                IQueryable<Order> orders = context.Orders;

                if (!string.IsNullOrEmpty(status))
                    orders = orders.Where(p => p.Status == status);
                else
                    orders = orders.Where(p => p.Status == "Pending");

                return orders.OrderBy(p => p.Id).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        //get count of list of orders
        public int GetOrdersCount( string status)
        {
            using (var context = new eComContext())
            {
                var orders = context.Orders.AsQueryable();

                if (!string.IsNullOrEmpty(status))
                    orders = orders.Where(p => p.Status == status);
                else
                    orders = orders.Where(p => p.Status == "Pending");

                return orders.Count();
            }
        }

        //get order by id
        public Order GetOrder(int id)
        {
            using (var context = new eComContext())
            {
               return context.Orders
                   .Where(o => o.Id == id).Include(o => o.OrderItems)
                   .Include("OrderItems.Product")
                   .FirstOrDefault();
            }
        }

        //edit order status (by admin)
        public bool UpdateOrderStatus(string status, int id)
        {
            using (var context = new eComContext())
            {
                var order = context.Orders.Where(o => o.Id == id).FirstOrDefault();

                order.Status = status;

                context.Entry(order).State = EntityState.Modified;
                return context.SaveChanges() > 0;
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
