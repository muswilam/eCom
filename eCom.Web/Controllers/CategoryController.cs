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
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult CategoryTable(string search)
        {
            CategorySearchViewModel catModel = new CategorySearchViewModel();

            catModel.Categories = CategoriesService.Instance.GetCategories();

            if (!string.IsNullOrEmpty(search))
            {
                catModel.Categories = catModel.Categories.Where(c => c.Name.ToLower().Contains(search.ToLower())).ToList();

                catModel.SearchTerm = search;
            }

            return PartialView("_CategoryTable", catModel);
        }

        #region Category Creation
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

            CategoriesService.Instance.SaveCategory(newCategory);

            return RedirectToAction("CategoryTable");
        }
        #endregion

        #region Category Updation
        [HttpGet]
        public PartialViewResult Edit(int id)
        {
            EditCategoryViewModel editModel = new EditCategoryViewModel();

            var categoryFromDb = CategoriesService.Instance.GetCategory(id);

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
            var categoryFromDb = CategoriesService.Instance.GetCategory(categoryModel.Id);

            categoryFromDb.Name = categoryModel.Name;
            categoryFromDb.Description = categoryModel.Description;
            categoryFromDb.ImageUrl = categoryModel.ImageUrl;
            categoryFromDb.IsFeatured = categoryModel.IsFeatured;

            CategoriesService.Instance.UpdateCategory(categoryFromDb);

            return RedirectToAction("CategoryTable");
        }
        #endregion

        [HttpPost]
        public ActionResult Delete(int id)
        {
            CategoriesService.Instance.DeleteCategory(id);

            return RedirectToAction("CategoryTable");
        }
    }
}