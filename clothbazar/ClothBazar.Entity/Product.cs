using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothBazar.Entity
{
   public class Product:BaseClass
    {
        public virtual Category Category { get; set; }
        public decimal Price { get; set; }
    }
}
