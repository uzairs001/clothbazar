using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothBazar.Entity
{
   public  class OrderItems
    {
        public int ID { get; set; }
        public int quantity { get; set; }
        public  int orderID { get; set; }
        public virtual Orders order { get; set; }
        public int ProductID { get; set; }
        public virtual Product product { get; set; }
    }
}
