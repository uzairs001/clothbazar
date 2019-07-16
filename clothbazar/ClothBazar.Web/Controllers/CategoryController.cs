using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClothBazar.Entity;
using ClothBazar.Services;

namespace ClothBazar.Web.Controllers
{
    public class CategoryController : Controller
    {
        CategoryService categoryService = new CategoryService();

        public ActionResult Index()
        {
            var categoryList = categoryService.GetCategory();
            return View(categoryList);
        }
        // GET: Category
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Category category)
        {
            categoryService.SaveCategory(category);
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int ID)
        {
            var categoryByID = categoryService.EditCategory(ID);
            return View(categoryByID);
        }
        [HttpPost]
        public ActionResult Edit(Category category)
        {
            categoryService.UpdateCategory(category); 
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int ID)
        {
            var category =  categoryService.EditCategory(ID);
            return View(category);
        }
        [HttpPost]
        public ActionResult Delete(Category category)
        {
            categoryService.DeleteCategory(category.ID);
            return RedirectToAction("Index");
        }

      
    }
}