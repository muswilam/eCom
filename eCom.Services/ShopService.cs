using eCom.Data;
using eCom.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCom.Services
{
    public class ShopService
    {
        #region Singleton Non-Thread Safety
        public static ShopService Instance
        {
            get
            {
                if (instance == null) instance = new ShopService();

                return instance;
            }
        }
        private static ShopService instance { get; set; }
        private ShopService()
        {
        }
        #endregion

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
