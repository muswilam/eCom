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
        // GET: Widgets
        public ActionResult Products(bool isLatestProducts)
        {
            var productWidgetModel = new ProductsWidgetViewModel();
            productWidgetModel.IsLatestProducts = isLatestProducts;
            productWidgetModel.FilledCategories = CategoriesService.Instance.GetFilledCategories();

            if(isLatestProducts)
            {
                productWidgetModel.Products = ProductService.Instance.GetLatestProducts(4);
            }
            else
            {
                productWidgetModel.Products = ProductService.Instance.GetProducts(1, 8);
            }
            return PartialView(productWidgetModel);
        }
    }
}