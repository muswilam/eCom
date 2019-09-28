﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eCom.Services;
using eCom.Entities;
using eCom.Web.ViewModels;

namespace eCom.Web.Controllers
{
    public class ProductController : Controller
    {
        ProductService PServices = new ProductService();

        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult ProductTable(string search)
        {
            var products = PServices.GetProducts();

            if(!string.IsNullOrEmpty(search))
                products = products.Where(p => p.Name.ToLower().Contains(search.ToLower())).ToList();
            
            return PartialView(products);
        }

        [HttpGet]
        public PartialViewResult Create()
        {
            CategoriesService CatServices = new CategoriesService();

            var categories = CatServices.GetCategories();

            return PartialView(categories);
        }

        [HttpPost]
        public ActionResult Create(NewCategoryViewModel productModel)
        {
            CategoriesService CatServices = new CategoriesService();

            var newProduct = new Product();
            newProduct.Name = productModel.Name;
            newProduct.Description = productModel.Description;
            newProduct.Price = productModel.Price;
            newProduct.Category = CatServices.GetCategory(productModel.CategoryId);

            PServices.SaveProduct(newProduct);

            return RedirectToAction("ProductTable");
        }

        [HttpGet]
        public PartialViewResult Edit(int id)
        {
            var product = PServices.GetProduct(id);
            return PartialView(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            PServices.UpdateProduct(product);

            return RedirectToAction("ProductTable");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            PServices.DeleteProduct(id);

            return RedirectToAction("ProductTable");
        }
    }
}