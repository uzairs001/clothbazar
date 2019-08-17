using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClothBazar.Web.ViewModels;
using ClothBazar.Services;

namespace ClothBazar.Web.Controllers
{
    public class WidgetsController : Controller
    {
        // GET: Widgets
        public ActionResult LatestProducts()
        {
            WidgetViewModel model = new WidgetViewModel();
            model.products = ProductService.Instance.GetLatestProduct(4);
            

            return PartialView(model);
        }
    }
}