using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eCom.Entities;
using eCom.Web.Models;

namespace eCom.Web.ViewModels
{
    public class CheckoutViewModel
    {
        public List<Product> CartProducts { get; set; }

        public List<int> CartProductIds { get; set; }
        public ApplicationUser User { get; set; }
    }

    public class ShopViewModel
    {
        public List<Product> Products { get; set; }
        public List<Category> FeaturedCategories { get; set; }
        public decimal MaximumPrice { get; set; }

        public byte? SortBy { get; set; }
        public int? CategoryId { get; set; }
        public string SearchTerm { get; set; }

        public Pager Pager { get; set; }
    }

    public class FilterProductViewModel
    {
        public List<Product> Products { get; set; }

        public byte? SortBy { get; set; }
        public int? CategoryId { get; set; }
        public string SearchTerm { get; set; }

        public Pager Pager { get; set; }
    }

    public class OrderPlacedViewModel
    {
        public ApplicationUser User { get; set; }
        public List<Order> Orders { get; set; }
    }
}