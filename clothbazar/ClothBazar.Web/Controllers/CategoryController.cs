using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClothBazar.Entity;
using ClothBazar.Services;
using ClothBazar.Web.ViewModels;

namespace ClothBazar.Web.Controllers
{
    public class CategoryController : Controller
    {
       

        public ActionResult Index()
        {

            return View();
        }

        public ActionResult CategoryTable(string search)
        {
          ProductCount productCount = new ProductCount();
            productCount.catList = CategoryService.Instance.GetCategory();
            Category cat = new Category();
           
            if (!string.IsNullOrEmpty(search))
            {

                productCount.catList = productCount.catList.Where(x => x.Name != null && x.Name.ToLower().Contains(search.ToLower())).ToList();
            }
            return PartialView(productCount);

        }
        // GET: Category
        [HttpGet]
        public ActionResult Create()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult Create(Category category)
        {
            CategoryService.Instance.SaveCategory(category);
            return RedirectToAction("CategoryTable");
        }

        [HttpGet]
        public ActionResult Edit(int ID)
        {
            var categoryByID = CategoryService.Instance.EditCategory(ID);
            return PartialView(categoryByID);
        }
        [HttpPost]
        public ActionResult Edit(Category category)
        {
            CategoryService.Instance.UpdateCategory(category);
            return RedirectToAction("CategoryTable");
        }

        [HttpGet]
        public ActionResult Delete(int ID)
        {
            var category = CategoryService.Instance.EditCategory(ID);
            CategoryService.Instance.DeleteCategory(category.ID);
            return RedirectToAction("CategoryTable");
        }
        //[HttpPost]
        //public ActionResult Delete(Category category)
        //{
        //    categoryService.DeleteCategory(category.ID);
        //    return RedirectToAction("Index");
        //}

      
    }
}