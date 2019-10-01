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
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult ProductTable(string search, int? pageNo)
        {
            ProductSearchViewModel productModel = new ProductSearchViewModel();

            productModel.PageNo = pageNo.HasValue && pageNo.Value > 0 ? pageNo.Value : 1;

            productModel.Products = ProductService.Instance.GetProducts(productModel.PageNo);

            if(!string.IsNullOrEmpty(search))
            {
                productModel.Products = productModel.Products.Where(p => p.Name.ToLower().Contains(search.ToLower())).ToList();

                productModel.SearchTerm = search;
            }
            
            return PartialView(productModel);
        }

        #region Product Creation
        [HttpGet]
        public PartialViewResult Create()
        {
            NewProductViewModel productModel = new NewProductViewModel();
             
            productModel.AvailableCategories = CategoriesService.Instance.GetCategories();

            return PartialView(productModel);
        }

        [HttpPost]
        public ActionResult Create(NewProductViewModel productModel)
        {
            var newProduct = new Product();
            newProduct.Name = productModel.Name;
            newProduct.Description = productModel.Description;
            newProduct.Price = productModel.Price;
            newProduct.Category = CategoriesService.Instance.GetCategory(productModel.CategoryId);

            ProductService.Instance.SaveProduct(newProduct);

            return RedirectToAction("ProductTable");
        }
        #endregion

        #region Product Updation
        [HttpGet]
        public PartialViewResult Edit(int id)
        {
            EditProductViewModel editModel = new EditProductViewModel();

            var productFromDb = ProductService.Instance.GetProduct(id);

            editModel.Id = productFromDb.Id;
            editModel.Name = productFromDb.Name;
            editModel.Description = productFromDb.Description;
            editModel.Price = productFromDb.Price;
            editModel.CategoryId = productFromDb.Category.Id;

            editModel.Categories = CategoriesService.Instance.GetCategories();

            return PartialView(editModel);
        }

        [HttpPost]
        public ActionResult Edit(EditProductViewModel productModel)
        {
            var productFromDb = ProductService.Instance.GetProduct(productModel.Id);

            productFromDb.Name = productModel.Name;
            productFromDb.Description = productModel.Description;
            productFromDb.Price = productModel.Price;
            productFromDb.CategoryId = productModel.CategoryId;
            productFromDb.Category = CategoriesService.Instance.GetCategory(productModel.CategoryId);

            ProductService.Instance.UpdateProduct(productFromDb);

            return RedirectToAction("ProductTable");
        }

        #endregion

        [HttpPost]
        public ActionResult Delete(int id)
        {
            ProductService.Instance.DeleteProduct(id);

            return RedirectToAction("ProductTable");
        }
    }
}