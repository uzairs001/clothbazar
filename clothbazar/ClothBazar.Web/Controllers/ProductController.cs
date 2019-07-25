using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClothBazar.Services;
using ClothBazar.Entity;
using ClothBazar.Web.ViewModels;

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
            CategoryService category = new CategoryService();
           List<Category> cat = category.GetFeaturedCategory();
           return PartialView(cat);
        }
        [HttpPost]
        public ActionResult Create(CategoryViewModel categoryViewModel)
        {
            CategoryService categoryService = new CategoryService();
            Product newproduct = new Product();
            newproduct.Name = categoryViewModel.Name;
            newproduct.Description = categoryViewModel.Description;
            newproduct.Price = categoryViewModel.Price;
            newproduct.Category =categoryService.EditCategory(categoryViewModel.CategoryID);
            

            productService.SaveProduct(newproduct);
            return RedirectToAction("ProductTable");
        }

        [HttpGet]
        public ActionResult Edit(int ID)
        {
            var productByID = productService.EditProduct(ID);
            return PartialView(productByID);
        }
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            productService.UpdateProduct(product);
            return RedirectToAction("ProductTable");
        }

        [HttpGet]
        public ActionResult Delete(int ID)
        {
            var product = productService.EditProduct(ID);
            productService.DeleteProduct(product.ID);
            return RedirectToAction("ProductTable");
        }
      
    }
}