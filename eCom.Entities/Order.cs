using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCom.Entities
{
    public class Order 
    {
        public int Id { get; set; }
        public string UserId { get; set; } //identityUser
        public DateTime OrderedAt { get; set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }

        //nav props
        public List<OrderItem> OrderItems { get; set; }
    }
}
