using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClothBazar.Entity;

namespace ClothBazar.Web.ViewModels
{
    public class OrdersViewModel
    {
        
        public List<Orders> order { get; set; }
        public int pageNo { get; set; }
        public int totalRecords { get; set; }
        public Pager pager { get; set; }
        public string UserID { get; set; }
    }
}