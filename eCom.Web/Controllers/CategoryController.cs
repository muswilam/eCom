﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eCom.Entities;
using eCom.Services;

namespace eCom.Web.Controllers
{
    public class CategoryController : Controller
    {
        CategoriesService CatServices = new CategoriesService();

        public ActionResult Index()
        {
            var categories = CatServices.GetCategories();

            return View(categories);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category category)
        {
            CatServices.SaveCategory(category);

            return RedirectToAction("Index");
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