using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eCom.Entities;
using System.ComponentModel.DataAnnotations;

namespace eCom.Web.ViewModels
{
    public class CategorySearchViewModel
    {
        public List<Category> Categories { get; set; }

        public string SearchTerm { get; set; }
        public Pager Pager { get; set; }
    }

    public class NewCategoryViewModel
    {
        [Required]
        [MinLength(5), MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public bool IsFeatured { get; set; }
    }

    public class EditCategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public bool IsFeatured { get; set; }
    }
}