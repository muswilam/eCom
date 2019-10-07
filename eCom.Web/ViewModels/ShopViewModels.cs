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

    public class ShopViewModel
    {
        public List<Product> Products { get; set; }
        public List<Category> FeaturedCategories { get; set; }
        public int MaximumPrice { get; set; }

    }
}