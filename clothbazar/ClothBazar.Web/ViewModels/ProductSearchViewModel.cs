using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClothBazar.Entity;

namespace ClothBazar.Web.ViewModels
{
    public class ProductSearchViewModel
    {
        public List<Product> Products { get; set; }
        public int pageNo { get; set; }
    }
}