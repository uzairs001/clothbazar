using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothBazar.Entity;
using ClothBazar.Database;

namespace ClothBazar.Services
{
    public class ShopService
    {
        private static ShopService instance { get; set; }
        public static ShopService Instance
        {
            get
            {
                if (instance == null) instance = new ShopService();
                return instance;
            }
        }
        private ShopService()
        {

        }

        public int SaveOrder(Orders order)
        {
            using (var context = new CBContext())
            {
                context.orders.Add(order);
                return context.SaveChanges();
            }
        }
    }
}
