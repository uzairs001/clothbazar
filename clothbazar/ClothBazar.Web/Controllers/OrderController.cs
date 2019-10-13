using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClothBazar.Web.ViewModels;
using ClothBazar.Services;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using ClothBazar.Entity;


namespace ClothBazar.Web.Controllers
{
    public class OrderController : Controller
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
        // GET: Order
        public ActionResult Index(string UserID, int? pageNo)
        {
            OrdersViewModel model = new OrdersViewModel();
            pageNo = (pageNo.HasValue) ? pageNo.Value > 0 ? pageNo : 1 : 1;
            model.pageNo = pageNo.Value;
            model.UserID = UserID;
           // model.searchTerm = search;
            int totalRecords = OrderService.Instance.getOrderCount(UserID);
             int pageSize = 3;
           
            //model.Products = ProductService.Instance.GetProduct(pageNo.Value);
             model.order = OrderService.Instance.GetOrders(UserID, pageNo.Value, pageSize);

            model.pager = new Pager(totalRecords, pageNo, pageSize);
            return View(model);


        }

        public ActionResult Details(int ID)
        {
            DetailViewModal model = new DetailViewModal();
            model.order = OrderService.Instance.getOrderByID(ID);
            model.user = UserManager.FindById(model.order.userID);
            model.AvailableStatus = new List<string> { "Pending", "InProcess", "Delivered" };
            return View(model);


        }

        public JsonResult UpdateOrderStatus(string status, int ID)
        {

            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;           
            result.Data = new { Success = OrderService.Instance.UpdateOrder(ID, status) };
            return result;


        }
    }
}