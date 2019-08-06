using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClothBazar.Entity;

namespace ClothBazar.Web.ViewModels
{
    public class CheckOutViewModel
    {
        public List<Product> cartProducts { get; set; }
        public List<int> ProductID { get; set; }
    }
}