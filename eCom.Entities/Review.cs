using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCom.Entities
{
    public class Review
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        [Required,MaxLength(500)]
        public string Body { get; set; }

        public DateTime ReviewedAt { get; set; }

        [Range(0,5)]
        public byte Rate { get; set; }

        //nav props
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}
