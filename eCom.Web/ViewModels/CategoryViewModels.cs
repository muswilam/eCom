using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eCom.Entities;

namespace eCom.Web.ViewModels
{
    public class CategorySearchViewModel
    {
        public List<Category> Categories { get; set; }

        public string SearchTerm { get; set; }
    }

    public class NewCategoryViewModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public bool IsFeatured { get; set; }
    }
}