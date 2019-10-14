using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eCom.Entities;
using eCom.Web.Models;
using System.ComponentModel.DataAnnotations;

namespace eCom.Web.ViewModels
{
    public class ProductSearchViewModel
    {
        public List<Product> Products { get; set; }
        public List<Category> Categories { get; set; }

        public string SearchTerm { get; set; }
        public int? CategoryId { get; set; }
        public Pager Pager { get; set; }
    }

    public class NewProductViewModel
    {
        [Required]
        [MinLength(5), MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Range(1, 1000000)]
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }

        public List<Category> AvailableCategories { get; set; }
    }

    public class EditProductViewModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(5), MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Range(1, 1000000)]
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }
        public List<Category> Categories { get; set; }
    }

    public class ProductViewModel
    {
        public Product Product { get; set; }

        public ApplicationUser User { get; set; }
    }
}