using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eCom.Entities;

namespace eCom.Web.ViewModels
{
    public class CheckoutViewModel
    {
        public List<Product> CartProducts { get; set; }

        public List<int> CartProductIds { get; set; }
    }
}