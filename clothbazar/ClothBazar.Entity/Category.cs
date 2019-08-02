using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothBazar.Entity
{
    public class Category:BaseClass
    {
        public virtual List<Product> Products { get; set; }
        public string ImageURL { get; set; }
        public bool IsFeatured { get; set; }
    }
}
