using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClothBazar.Services;
using ClothBazar.Web.ViewModels;
using ClothBazar.Entity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
namespace ClothBazar.Web.Controllers
{
    
    public class ShopController : Controller
    {
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
        [Authorize]
        public ActionResult CheckOut()
        {
            CheckOutViewModel model = new CheckOutViewModel();

           var cartProduct = Request.Cookies["myAddToCartCookie"];
            if (cartProduct != null && !string.IsNullOrEmpty(cartProduct.Value))
           {
               var cartProducts = cartProduct.Value;
               var cartItemInString = cartProducts.Split('-');
               List<int> cartItem = cartItemInString.Select(x =>int.Parse(x)).ToList();
               model.cartProducts = ProductService.Instance.GetCartProduct(cartItem);
               model.ProductID = cartItem;
               model.User = UserManager.FindById(User.Identity.GetUserId());
           }
            return View(model);
        }

        public JsonResult PlaceOrder(string Products)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            if (!string.IsNullOrEmpty(Products)) 
            {
                
                Orders orders = new Orders();
                var a = Products.Split('-').ToList();
                var b = a.Select(x => int.Parse(x)).ToList();
                var ProductIDs = a.Select(x => int.Parse(x)).Distinct().ToList();
                orders.userID = User.Identity.GetUserId();
                orders.status = "Pending";
                orders.dateTime = DateTime.Now;
                List<Product> cartProduct = ProductService.Instance.GetCartProduct(ProductIDs);
                orders.totalAmount = cartProduct.Sum(x => x.Price * (b.Where(y => y == x.ID).Count()));
                orders.orderItems = new List<OrderItems>();
                orders.orderItems.AddRange(cartProduct.Select(x => new OrderItems() { ProductID = x.ID, quantity = (b.Where(y => y == x.ID).Count()) }));
                int rowseffected = ShopService.Instance.SaveOrder(orders);

                result.Data = new { Success = true, Rows = rowseffected };
            }
           else
            {
                result.Data = new { Success = false };
            }

           return result;
        }
    }
}