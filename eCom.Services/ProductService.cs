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
        #region Singleton
        public static ProductService Instance
        {
            get
            {
                if (instance == null) instance = new ProductService();

                return instance;
            }
        }
        private static ProductService instance { get; set; }
        private ProductService()
        {
        }
        #endregion

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
        public List<Product> GetProducts(int pageNo)
        {
            int pageSize = 5;

            using (var context = new eComContext())
            {
                //return context.Products.OrderBy(p => p.Id).Skip((pageNo-1)*pageSize).Take(pageSize).Include(p => p.Category).ToList();

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
