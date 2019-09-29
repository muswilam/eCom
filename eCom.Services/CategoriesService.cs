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
    public class CategoriesService
    {
        public Category GetCategory(int id)
        {
            using (var context = new eComContext())
            {
                return context.Categories.Where(c => c.Id == id).FirstOrDefault();
            }
        }

        public List<Category> GetCategories()
        {
            using (var context = new eComContext())
            {
                return context.Categories.Include(c => c.Products).ToList();
            }
        }

        public List<Category> GetFeaturedCategories()
        {
            using (var context = new eComContext())
            {
                return context.Categories.Where(c => c.IsFeatured && c.ImageUrl != null).ToList();
            }
        }

        public void SaveCategory(Category category)
        {
            using (var context = new eComContext())
            {
                context.Categories.Add(category);
                context.SaveChanges();
            }
        }

        public void UpdateCategory(Category category)
        {
            using (var context = new eComContext())
            {
                context.Entry(category).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

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
