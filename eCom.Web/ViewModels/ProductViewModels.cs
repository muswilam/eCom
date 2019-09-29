using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eCom.Web.ViewModels
{
    public class NewProductViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public int CategoryId { get; set; }
    }
}