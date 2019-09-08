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

        public ActionResult Index(string searchTerm, int? minimumPrice, int? maximumPrice, int? categoryId, int? sortBy, int? pageNo)
        {
            FilterProductViewModel model = new FilterProductViewModel();
               pageNo = (pageNo.HasValue) ? pageNo.Value > 0 ? pageNo : 1 : 1;
               model.pageSize = 6;
               model.searchTerm = searchTerm;
               int count = ProductService.Instance.GetProductCount(searchTerm, minimumPrice, maximumPrice, categoryId);
            model.categoryList = CategoryService.Instance.GetCategoryHavingProduct();
            model.products = ProductService.Instance.ProductsForShopPage(searchTerm, maximumPrice, minimumPrice, categoryId, sortBy, pageNo.Value, model.pageSize);
            model.CatID = categoryId;
            model.sortByNumber = sortBy;
            model.maxPrice = ProductService.Instance.GetMaxPrice();
            model.pager = new Pager(count, pageNo, model.pageSize);
           

            return View(model);

        }

        public ActionResult FilterProduct(string searchTerm, int? filteredmaximumPrice, int? filteredminimumPrice, int? categoryId, int? sortBy, int? pageNo)
          {
              SliderFilterProductViewModel model = new SliderFilterProductViewModel();
              int pageSize = 6;
              pageNo = (pageNo.HasValue) ? pageNo.Value > 0 ? pageNo : 1 : 1;
              model.filterProduct = ProductService.Instance.ProductsForShopPage(searchTerm, filteredmaximumPrice, filteredminimumPrice, categoryId, sortBy, pageNo.Value, pageSize);
             
              decimal maxPrice = ProductService.Instance.GetMaxPrice();
              int? minimumPrice = (filteredminimumPrice.HasValue) ? filteredminimumPrice.Value != 0 ? filteredminimumPrice : 0 : 0;
              decimal? maximumPrice = (filteredminimumPrice.HasValue) ? filteredmaximumPrice.Value == maxPrice ? maxPrice : filteredmaximumPrice.Value : 0;
              
              model.SortBy = sortBy;
              model.CategoryID = categoryId;

              int count = ProductService.Instance.GetProductCount(searchTerm, minimumPrice, maximumPrice, categoryId);
              model.pager = new Pager(count, pageNo, pageSize);

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