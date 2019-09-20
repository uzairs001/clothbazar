using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ClothBazar.Entity
{
    public class Orders
    {
        public int ID { get; set; }
        public string userID { get; set; }
        public DateTime dateTime { get; set; }
        public string status { get; set; }
        public List<OrderItems> orderItems { get; set; }
        public decimal totalAmount { get; set; }
    }
}
