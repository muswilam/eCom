using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCom.Entities
{
    public class Product : BaseEntity
    {
        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        //nav prop
        public Category Category { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
    }
}
