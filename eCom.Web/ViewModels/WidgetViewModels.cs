using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eCom.Entities;

namespace eCom.Web.ViewModels
{
    public class ProductsWidgetViewModel
    {
        public List<Product> Products { get; set; }
        public bool IsLatestProducts { get; set; }
        public List<Category> FeaturedCategories { get; set; }
        public bool isCategoryId { get; set; }
    }
}