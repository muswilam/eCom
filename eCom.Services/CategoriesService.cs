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
    [System.Runtime.InteropServices.GuidAttribute("891707B3-DC63-483D-8CEB-3261A81925CD")]
    public class CategoriesService
    {
        #region Singleton Non-Thread Safety
        public static CategoriesService Instance
        {
            get
            {
                if (instance == null) instance = new CategoriesService();

                return instance;
            }
        }
        private static CategoriesService instance { get; set; }
        private CategoriesService()
        {
        }
        #endregion

        //get category by id
        public Category GetCategory(int id)
        {
            using (var context = new eComContext())
            {
                return context.Categories.Where(c => c.Id == id).FirstOrDefault();
            }
        }

        //get all categoies 
        public List<Category> GetCategories()
        {
            using (var context = new eComContext())
            {
                return context.Categories.Include(c => c.Products).ToList();
            }
        }

        //get categories by search and pageNo if exist
        public List<Category> GetCategories(string search, int pageNo, int pageSize)
        {
            using (var context = new eComContext())
            {
                if (!string.IsNullOrEmpty(search))
                {
                    return context.Categories
                        .Where(c => c.Name != null && c.Name.ToLower().Contains(search.ToLower()))
                        .OrderBy(c => c.Id)
                        .Skip((pageNo - 1) * pageSize)
                        .Take(pageSize)
                        .Include(c => c.Products)
                        .ToList();
                }
                return context.Categories.OrderBy(c => c.Id).Skip((pageNo -1)* pageSize).Take(pageSize).Include(c => c.Products).ToList();
            }
        }   

        //get categories that have products
        public List<Category> GetFilledCategories()
        {
            using (var context = new eComContext())
            {
                return context.Categories.Include(c => c.Products).Where(c => c.Products.Count() > 0).ToList();
            }
        }

        //get categories count
        public int GetCategoriesCount(string search)
        {
            using (var context = new eComContext())
            {
                if (!string.IsNullOrEmpty(search))
                {
                    return context.Categories
                        .Where(c => c.Name != null && c.Name.ToLower().Contains(search.ToLower())).Count();
                }
                return context.Categories.Count();
            }
        }

        //get list of featured categoriess
        public List<Category> GetFeaturedCategories()
        {
            using (var context = new eComContext())
            {
                return context.Categories.Where(c => c.IsFeatured && c.ImageUrl != null).ToList();
            }
        }

        //create category
        public void SaveCategory(Category category)
        {
            using (var context = new eComContext())
            {
                context.Categories.Add(category);
                context.SaveChanges();
            }
        }

        //edit category
        public void UpdateCategory(Category category)
        {
            using (var context = new eComContext())
            {
                context.Entry(category).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        //delete category
        public void DeleteCategory(int id)
        {
            using (var context = new eComContext())
            {
                var category = context.Categories.Find(id);

                context.Categories.Remove(category);
                context.SaveChanges();
            }
        }

    }
}
