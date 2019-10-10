using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eCom.Entities;

namespace eCom.Web.ViewModels
{
    public class OrdersViewModels
    {
        public List<Order> Orders { get; set; }

        public string UserId { get; set; }

        public Pager Pager { get; set; }

        public string Status { get; set; }
    }
}