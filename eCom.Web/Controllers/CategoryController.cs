using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using eCom.Entities;
using eCom.Services;
using eCom.Web.ViewModels;

namespace eCom.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CategoryTable(string search, int? pageNo)
        {
            var pageSize = ConfigurationService.Instance.PageSize();
            CategorySearchViewModel catModel = new CategorySearchViewModel();
    
            catModel.SearchTerm = search;
            pageNo = pageNo.HasValue && pageNo.Value > 0 ? pageNo.Value : 1;

            var totalRecords = CategoriesService.Instance.GetCategoriesCount(search);

            catModel.Categories = CategoriesService.Instance.GetCategories(search, pageNo.Value, pageSize);
       
            catModel.Pager = new Pager(totalRecords, pageNo, pageSize);

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
             if (!ModelState.IsValid)
             {
                 return new HttpStatusCodeResult(500);
             }

             var newCategory = new Category();
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
            if (!ModelState.IsValid)
            {
                return new HttpStatusCodeResult(500);
            }

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