using System;
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
        //ProductService PServices = new ProductService();
        CategoriesService CatServices = new CategoriesService();

        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult ProductTable(string search)
        {
            ProductSearchViewModel productModel = new ProductSearchViewModel();

            productModel.Products =  ProductService.Instance.GetProducts();

            if(!string.IsNullOrEmpty(search))
            {
                productModel.Products = productModel.Products.Where(p => p.Name.ToLower().Contains(search.ToLower())).ToList();

                productModel.SearchTerm = search;
            }
            
            return PartialView(productModel);
        }

        #region Creation

        [HttpGet]
        public PartialViewResult Create()
        {
            NewProductViewModel productModel = new NewProductViewModel();
             
            productModel.AvailableCategories = CatServices.GetCategories();

            return PartialView(productModel);
        }

        [HttpPost]
        public ActionResult Create(NewProductViewModel productModel)
        {
            var newProduct = new Product();
            newProduct.Name = productModel.Name;
            newProduct.Description = productModel.Description;
            newProduct.Price = productModel.Price;
            newProduct.Category = CatServices.GetCategory(productModel.CategoryId);

            ProductService.Instance.SaveProduct(newProduct);

            return RedirectToAction("ProductTable");
        }
        #endregion

        #region Updation

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

            editModel.Categories = CatServices.GetCategories();

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
            productFromDb.Category = CatServices.GetCategory(productModel.CategoryId);

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