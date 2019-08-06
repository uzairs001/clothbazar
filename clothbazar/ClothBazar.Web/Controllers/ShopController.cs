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
    public class ShopController : Controller
    {
        // GET: Shop
        public ActionResult CheckOut()
        {
            CheckOutViewModel model = new CheckOutViewModel();
            ProductService Pservice = new ProductService();
           var cartProduct =  Request.Cookies["addToCartCookie"];
           if (cartProduct != null)
           {
               var cartProducts = cartProduct.Value;
               var cartItemInString = cartProducts.Split('-');
               List<int> cartItem = cartItemInString.Select(x =>int.Parse(x)).ToList();
               model.cartProducts = Pservice.GetCartProduct(cartItem);
               model.ProductID = cartItem;

           }
            return View(model);
        }
    }
}