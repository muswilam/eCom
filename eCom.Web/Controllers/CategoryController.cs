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
        public ActionResult Edit(int id)
        {
            var category = CatServices.GetCategory(id);

            return View(category);
        }

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            CatServices.UpdateCategory(category);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var category = CatServices.GetCategory(id);

            return View(category);
        }

        [HttpPost]
        public ActionResult Delete(Category categoryModel)
        {
            CatServices.DeleteCategory(categoryModel.Id);

            return RedirectToAction("Index");
        }
    }
}