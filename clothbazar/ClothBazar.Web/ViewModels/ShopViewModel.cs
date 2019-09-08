using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClothBazar.Entity;

namespace ClothBazar.Web.ViewModels
{
    public class FilterProductViewModel
    {
        public List<Category> categoryList { get; set; }
        public List<Product> products { get; set; }
        public int? CatID { get; set; }
        public int? sortByNumber { get; set; }
        public decimal maxPrice { get; set; }
        public Pager pager { get; set; }
        public int pageSize { get; set; }

        public string searchTerm { get; set; }
    }

    public class SliderFilterProductViewModel
    {
        public List<Product> filterProduct { get; set; }
        public Pager pager { get; set; }
        public int? SortBy { get; set; }
        public int? CategoryID { get; set; }      
    }

}