using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eCom.Entities;

namespace eCom.Web.ViewModels
{
    public class ProductSearchViewModel
    {
        public List<Product> Products { get; set; }

        public string SearchTerm { get; set; }
        public Pager Pager { get; set; }
    }

    public class NewProductViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }

        public List<Category> AvailableCategories { get; set; }
    }

    public class EditProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }
        public List<Category> Categories { get; set; }
    }
}