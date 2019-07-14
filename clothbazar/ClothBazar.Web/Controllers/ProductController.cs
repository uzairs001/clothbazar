using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClothBazar.Services;
using ClothBazar.Entity;

namespace ClothBazar.Web.Controllers
{
    public class ProductController : Controller
    {
        ProductService productService = new ProductService();
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductTable(string search)
        {


            var product = productService.GetProduct();
            if (string.IsNullOrEmpty(search) == false)
            {
                product = product.Where(x => x.Name != null && x.Name.ToLower().Contains(search.ToLower())).ToList();

            }
            return PartialView(product);


        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Product product)
        {
            productService.SaveProduct(product);
            return RedirectToAction("ProductTable");
        }
    }
}