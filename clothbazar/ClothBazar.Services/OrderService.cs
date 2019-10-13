using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothBazar.Entity;
using ClothBazar.Database;
using System.Data.Entity;

namespace ClothBazar.Services
{
   public class OrderService
    {
        private static OrderService instance { get; set; }
        public static OrderService Instance
        {
            get 
            {
                if (instance == null) instance = new OrderService();
                return instance;
            }
        }

        private OrderService()
        {

        }

        public int getOrderCount(string UserID)
        {
            using(var context = new CBContext())
            {
               var totalCount =  context.orders.ToList().Count;
               if (!string.IsNullOrEmpty(UserID))
              {
                  totalCount = context.orders.Where(x => x.userID == UserID).ToList().Count;
              }
              return totalCount;
            }

            
        }

        public List<Orders> GetOrders(string UserID, int pageNo, int pageSize)
        {

            //var context = new CBContext();
            //return context.Products.Include(x => x.Category).ToList();
            using (var context = new CBContext())
            {
                var order = context.orders.ToList();
                if (string.IsNullOrEmpty(UserID) == false)
                {

                    order = context.orders.Where(x => x.userID != null && x.userID.ToLower().Contains(UserID.ToLower())).ToList();

                }
                order = order.OrderBy(x => x.ID).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
                return order;

            }
        }

        public Orders getOrderByID(int ID)
        {
            //var context = new CBContext();
            //return context.Products.Find(ID);
            using (var context = new CBContext())
            {
                return context.orders.Where(x => x.ID == ID).Include(y => y.orderItems).Include("orderItems.Product").FirstOrDefault();
                
            }
        }

        public bool UpdateOrder(int ID, string status)
        {
            using (var context = new CBContext())
            {
                var order = context.orders.Find(ID);
                order.status = status;
                return context.SaveChanges() > 0;

            }
        }
    }
}
