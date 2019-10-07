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
        #region Singleton Non-Thread Safety 
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

        //get maximum price
        public decimal GetMaxPrice()
        {
            using (var context = new eComContext())
            {
                return context.Products.Max(p => p.Price);
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
        public List<Product> GetProducts(string search, int pageNo)
        {
            int pageSize = 4;

            using (var context = new eComContext())
            {
                if (!string.IsNullOrEmpty(search))
                {
                    return context.Products
                        .Where(p => p.Name != null && p.Name.ToLower().Contains(search.ToLower()))
                        .OrderBy(p => p.Id)
                        .Skip((pageNo - 1) * pageSize)
                        .Take(pageSize)
                        .Include(p => p.Category)
                        .ToList();
                }
                return context.Products.OrderBy(p => p.Id).Skip((pageNo - 1) * pageSize).Take(pageSize).Include(p => p.Category).ToList();
            }
        }

        //get products by pageNo and pageSize
        public List<Product> GetProducts(int pageNo, int pageSize)
        {
            using (var context = new eComContext())
            {
                return context.Products.OrderByDescending(p => p.Id).Skip((pageNo - 1) * pageSize).Take(pageSize).Include(p => p.Category).ToList();
            }
        }

        //get products by category id
        public List<Product> GetProductsByCategory(int categoyId, int pageSize)
        {
            using (var context = new eComContext())
            {
                return context.Products.Where(p => p.CategoryId == categoyId).OrderByDescending(p => p.Id).Take(pageSize).Include(p => p.Category).ToList();
            }
        }

        //get latest products
        public List<Product> GetLatestProducts(int numberOfProducts)
        {
            using (var context = new eComContext())
            {
                return context.Products.OrderByDescending(p => p.Id).Take(numberOfProducts).Include(p => p.Category).ToList();
            }
        }

        //get list of products in shop by search, minPrice, maxPrice, categoryId
        public List<Product> GetShopProducts(string searchTerm, decimal? minPrice, decimal? maxPrice, int? categoryId,  byte? sortBy)
        {
            using (var context = new eComContext())
            {
                IQueryable<Product> products = context.Products;

                if (categoryId.HasValue)
                    products = products.Where(p => p.CategoryId == categoryId.Value);

                if(!string.IsNullOrEmpty(searchTerm))
                    products = products.Where(p => p.Name.ToLower().Contains(searchTerm.ToLower()));

                if (minPrice.HasValue)
                    products = products.Where(p => p.Price >= minPrice.Value);

                if (maxPrice.HasValue)
                    products = products.Where(p => p.Price <= maxPrice.Value);

                if(sortBy.HasValue)
                {
                    switch(sortBy.Value)
                    {
                        case 2: //popularity
                            products = products.OrderByDescending(p => p.Id);
                            break;
                        case 3: //low to high
                            products = products.OrderBy(p => p.Price);
                            break;
                        default: //default
                            products = products.OrderByDescending(p => p.Price);
                            break;
                    }
                }

                return products.ToList();
            }
        }

        //get products count
        public int GetProductsCount(string search)
        {
            using (var context = new eComContext())
            {
                if (!string.IsNullOrEmpty(search))
                {
                    return context.Products.Where(p => p.Name != null && p.Name.ToLower().Contains(search.ToLower())).Count();
                }
                return context.Products.Count();
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
