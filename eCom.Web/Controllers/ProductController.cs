using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using eCom.Services;
using eCom.Entities;
using eCom.Web.ViewModels;

namespace eCom.Web.Controllers
{
    public class ProductController : Controller
    {
        #region User Manager
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        #endregion

        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductTable(string search,int? categoryId, int? pageNo)
        {
            var pageSize = ConfigurationService.Instance.PageSize();

            ProductSearchViewModel productModel = new ProductSearchViewModel();

            pageNo = pageNo.HasValue && pageNo.Value > 0 ? pageNo.Value : 1;

            productModel.SearchTerm = search;
            productModel.CategoryId = categoryId;
            var totalRecords = ProductService.Instance.GetProductsCount(search, categoryId);

            productModel.Products = ProductService.Instance.GetProducts(search , categoryId, pageNo.Value, pageSize);
            productModel.Categories = CategoriesService.Instance.GetCategories();

            if(productModel.Products != null)
            {
                productModel.Pager = new Pager(totalRecords, pageNo.Value, pageSize);

                return PartialView(productModel);
            }

            return HttpNotFound();
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
            if (!ModelState.IsValid)
            {
                return new HttpStatusCodeResult(500);
            }

            var newProduct = new Product();
            newProduct.Name = productModel.Name;
            newProduct.Description = productModel.Description;
            newProduct.Price = productModel.Price;
            newProduct.ImageUrl = productModel.ImageUrl;
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
            editModel.ImageUrl = productFromDb.ImageUrl;
            editModel.CategoryId = productFromDb.Category != null ? productFromDb.Category.Id : 0;

            editModel.Categories = CategoriesService.Instance.GetCategories();

            return PartialView(editModel);
        }

        [HttpPost]
        public ActionResult Edit(EditProductViewModel productModel)
        {
            if(!ModelState.IsValid)
            {
                return new HttpStatusCodeResult(500);
            }

            var productFromDb = ProductService.Instance.GetProduct(productModel.Id);

            productFromDb.Name = productModel.Name;
            productFromDb.Description = productModel.Description;
            productFromDb.Price = productModel.Price;
            productFromDb.ImageUrl = productModel.ImageUrl;
            productFromDb.CategoryId = productModel.CategoryId;
            productFromDb.Category = CategoriesService.Instance.GetCategory(productModel.CategoryId);

            ProductService.Instance.UpdateProduct(productFromDb);

            return RedirectToAction("ProductTable");
        }

        #endregion

        [HttpGet]
        public ActionResult Details(int id)
        {
            ProductViewModel productModel = new ProductViewModel();

            productModel.Product = ProductService.Instance.GetProduct(id);
            productModel.AuthenticatedUser = UserManager.FindById(User.Identity.GetUserId());
            productModel.User = UserManager;

            if(productModel.AuthenticatedUser != null)
               productModel.IsProductWished = WishlistService.Instance.IsProductWished(productModel.AuthenticatedUser.Id, id);//check if user has wished this product before

            return View(productModel);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            ProductService.Instance.DeleteProduct(id);

            return RedirectToAction("ProductTable");
        }
    }
}