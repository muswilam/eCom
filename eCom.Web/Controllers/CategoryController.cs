using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eCom.Entities;
using eCom.Services;
using eCom.Web.ViewModels;

namespace eCom.Web.Controllers
{
    public class CategoryController : Controller
    {
        CategoriesService CatServices = new CategoriesService();

        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult CategoryTable(string search)
        {
            CategorySearchViewModel catModel = new CategorySearchViewModel();

            catModel.Categories = CatServices.GetCategories();

            if (!string.IsNullOrEmpty(search))
            {
                catModel.Categories = catModel.Categories.Where(c => c.Name.ToLower().Contains(search.ToLower())).ToList();

                catModel.SearchTerm = search;
            }

            return PartialView("_CategoryTable", catModel);
        }

        [HttpGet]
        public PartialViewResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Create(NewCategoryViewModel categoryModel)
         {
            Category newCategory = new Category();
            newCategory.Name = categoryModel.Name;
            newCategory.Description = categoryModel.Description;
            newCategory.ImageUrl = categoryModel.ImageUrl;
            newCategory.IsFeatured = categoryModel.IsFeatured;

            CatServices.SaveCategory(newCategory);

            return RedirectToAction("CategoryTable");
        }

        [HttpGet]
        public PartialViewResult Edit(int id)
        {
            EditCategoryViewModel editModel = new EditCategoryViewModel();

            var categoryFromDb = CatServices.GetCategory(id);

            editModel.Id = categoryFromDb.Id;
            editModel.Name = categoryFromDb.Name;
            editModel.Description = categoryFromDb.Description;
            editModel.ImageUrl = categoryFromDb.ImageUrl;
            editModel.IsFeatured = categoryFromDb.IsFeatured;

            return PartialView(editModel);
        }

        [HttpPost]
        public ActionResult Edit(EditCategoryViewModel categoryModel)
        {
            var categoryFromDb = CatServices.GetCategory(categoryModel.Id);

            categoryFromDb.Name = categoryModel.Name;
            categoryFromDb.Description = categoryModel.Description;
            categoryFromDb.ImageUrl = categoryModel.ImageUrl;
            categoryFromDb.IsFeatured = categoryModel.IsFeatured;

            CatServices.UpdateCategory(categoryFromDb);

            return RedirectToAction("CategoryTable");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            CatServices.DeleteCategory(id);

            return RedirectToAction("CategoryTable");
        }
    }
}