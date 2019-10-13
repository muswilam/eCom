using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eCom.Entities;
using eCom.Web.Models;

namespace eCom.Web.ViewModels
{
    public class OrdersViewModels
    {
        public List<Order> Orders { get; set; }

        public Pager Pager { get; set; }
        public string Status { get; set; }

        public List<string> AvailableStatus { get; set; }
    }

    public class OrderDetailsViewModel
    {
        public Order Order { get; set; }
        public ApplicationUser orderUser { get; set; }

        public List<string> AvailableStatuses { get; set; }
    }
}