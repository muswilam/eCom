using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eCom.Web.ViewModels;
using eCom.Services;

namespace eCom.Web.Controllers
{
    public class WidgetsController : Controller
    {
        public ActionResult Products(bool isLatestProducts, int? categoryId)
        {
            var productWidgetModel = new ProductsWidgetViewModel();
            productWidgetModel.IsLatestProducts = isLatestProducts;
            productWidgetModel.FilledCategories = CategoriesService.Instance.GetFilledCategories();
            productWidgetModel.isCategoryId = categoryId.HasValue;

            if(isLatestProducts)
            {
                productWidgetModel.Products = ProductService.Instance.GetLatestProducts(4);
            }
            else if(categoryId.HasValue && categoryId.Value > 0)
            {
                productWidgetModel.Products = ProductService.Instance.GetProductsByCategory(categoryId.Value, 4);
            }
            else
            {
                productWidgetModel.Products = ProductService.Instance.GetProducts(4);
            }
            return PartialView(productWidgetModel);
        }
    }
}