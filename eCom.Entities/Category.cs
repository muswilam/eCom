using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCom.Entities
{
    public class Category : BaseEntity
    {
        public string ImageUrl { get; set; }

        //nav prop
        public List<Product> Products { get; set; }
    }
}
