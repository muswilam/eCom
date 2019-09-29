using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using eCom.Entities;
using eCom.Data;

namespace eCom.Services
{
    public class ProductService
    {
        public Product GetProduct(int id)
        {
            using (var context = new eComContext())
            {
                return context.Products.Include(p => p.Category).Where(c => c.Id == id).FirstOrDefault();
            }
        }

        public List<Product> GetProducts()
        {
            using (var context = new eComContext())
            {
                return context.Products.Include(p => p.Category).ToList();
            }
        }

        public void SaveProduct(Product product)
        {
            using (var context = new eComContext())
            {
                context.Entry(product.Category).State = EntityState.Unchanged; //prevent adding category again with different id

                context.Products.Add(product);
                context.SaveChanges();
            }
        }

        public void UpdateProduct(Product product)
        {
            using (var context = new eComContext())
            {
                context.Entry(product).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteProduct(int id)
        {
            using (var context = new eComContext())
            {
                var product = context.Products.Find(id);

                context.Products.Remove(product);
                context.SaveChanges();
            }
        }
    }
}
