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
        //get product by id
        public Product GetProduct(int id)
        {
            using (var context = new eComContext())
            {
                return context.Products.Include(p => p.Category).Where(c => c.Id == id).FirstOrDefault();
            }
        }

        //get products by list of ids
        public List<Product> GetProducts(List<int> ids)
        {
            using (var context = new eComContext())
            {
                return context.Products.Where(p => ids.Contains(p.Id)).ToList();
            }
        }

        //get products 
        public List<Product> GetProducts()
        {
            using (var context = new eComContext())
            {
                return context.Products.Include(p => p.Category).ToList();
            }
        }

        //add product 
        public void SaveProduct(Product product)
        {
            using (var context = new eComContext())
            {
                context.Entry(product.Category).State = EntityState.Unchanged; //prevent adding category again with different id

                context.Products.Add(product);
                context.SaveChanges();
            }
        }

        //edit product
        public void UpdateProduct(Product product)
        {
            using (var context = new eComContext())
            {
                context.Entry(product).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        //delete product
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
