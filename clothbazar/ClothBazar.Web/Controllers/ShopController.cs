using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClothBazar.Services;
using ClothBazar.Web.ViewModels;
using ClothBazar.Entity;
namespace ClothBazar.Web.Controllers
{
    [Authorize]
    public class ShopController : Controller
    {

          public ActionResult Index(string searchTerm, int? maximumPrice, int? minimumPrice , int? CategoryID,int? sortBy)
        {
            FilterProductViewModel model = new FilterProductViewModel();
            model.categoryList = CategoryService.Instance.GetFeaturedCategory();
            model.products = ProductService.Instance.ProductsForShopPage(searchTerm, maximumPrice, minimumPrice, CategoryID,sortBy);
            model.CatID = CategoryID;
            model.sortByNumber = sortBy;
            model.maxPrice = ProductService.Instance.GetMaxPrice();

           

            return View(model);

        }

          public ActionResult FilterProduct(string searchTerm, int? filteredmaximumPrice, int? filteredminimumPrice, int? CategoryID, int? sortBy)
          {
              SliderFilterProductViewModel model = new SliderFilterProductViewModel();
              model.filterProduct = ProductService.Instance.ProductsForShopPage(searchTerm, filteredmaximumPrice, filteredminimumPrice, CategoryID, sortBy);



              return PartialView(model);

          }
        // GET: Shop
        public ActionResult CheckOut()
        {
            CheckOutViewModel model = new CheckOutViewModel();
          
           var cartProduct =  Request.Cookies["addToCartCookie"];
           if (cartProduct != null)
           {
               var cartProducts = cartProduct.Value;
               var cartItemInString = cartProducts.Split('-');
               List<int> cartItem = cartItemInString.Select(x =>int.Parse(x)).ToList();
               model.cartProducts = ProductService.Instance.GetCartProduct(cartItem);
               model.ProductID = cartItem;

           }
            return View(model);
        }
    }
}