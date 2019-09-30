using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCom.Entities;

namespace eCom.Data
{
    public class eComContext : DbContext
    {
        public eComContext()
            : base("name=eComContext")
        {}

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Config> Configurations { get; set; }
    }
}
