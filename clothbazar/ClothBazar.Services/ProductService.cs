using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothBazar.Database;
using ClothBazar.Entity;
using System.Data.Entity;

namespace ClothBazar.Services
{
   public class ProductService
    {
       private static ProductService instance { get; set; }
       public static ProductService Instance 
       {
         get  {
             if (instance == null) instance = new ProductService();
             return instance;
           }
       }
       private ProductService()
       {

       }
       public void SaveProduct(Product product)
        {
            using (var context = new CBContext())
            {
                context.Entry(product.Category).State = System.Data.Entity.EntityState.Unchanged;
                context.Products.Add(product);
                context.SaveChanges();
            }
        }

       public List<Product> GetProduct()
       {
         
           //var context = new CBContext();
           //return context.Products.Include(x => x.Category).ToList();
           using (var context = new CBContext())
           {
               return context.Products.Include(x => x.Category).ToList();

           }
       }

       public List<Product> GetProduct(int pageNo)
        {
            int pageSize = 3;
            //var context = new CBContext();
            //return context.Products.Include(x => x.Category).ToList();
            using (var context = new CBContext())
            {
                return context.Products.OrderBy(x=>x.ID).Skip((pageNo - 1) * pageSize).Take(pageSize).Include(x => x.Category).ToList();

            }
        }


       public List<Product> GetBestSaleProduct(int pageNo,int pageSize)
       {
          
           using (var context = new CBContext())
           {
               return context.Products.OrderByDescending(x => x.ID).Skip((pageNo - 1) * pageSize).Take(pageSize).Include(x => x.Category).ToList();

           }
       }

       public List<Product> GetLatestProduct(int count)
       {

           //var context = new CBContext();
           //return context.Products.Include(x => x.Category).ToList();
           using (var context = new CBContext())
           {
               return context.Products.OrderByDescending(x => x.ID).Take(count).Include(x => x.Category).ToList();

           }
       }
            public List<Product> GetRelatedProduct(int count,Category categ)
       {
           
           //var context = new CBContext();
           //return context.Products.Include(x => x.Category).ToList();
           using (var context = new CBContext())
           {
               return context.Products.Where(x=>x.Category.Name == categ.Name).OrderByDescending(x => x.ID).Take(count).Include(x => x.Category).ToList();

           }
       }



       public List<Product> GetCartProduct(List<int> IDs)
       {

        
           using (var context = new CBContext())
           {
               return context.Products.Where(x => IDs.Contains(x.ID)).ToList();

           }
       }

     
       public Product EditProduct(int ID)
        {
            var context = new CBContext();
            return context.Products.Find(ID);
            //using (var context = new CBContext())
            //{
            //    return context.Products.Find(ID);

            //}
        }

       public void UpdateProduct(Product product)
       {
           using (var context = new CBContext())
           {
               context.Entry(product).State = System.Data.Entity.EntityState.Modified;
               context.SaveChanges();

           }
       }

       public void DeleteProduct(int ID)
        {
            using (var context = new CBContext())
            {
                var product = context.Products.Find(ID);
                context.Products.Remove(product);
                context.SaveChanges();
            }
        }





       public List<Product> ProductsForShopPage(string searchTerm, int? maximumPrice, int? minimumPrice, int? CategoryID,int? sortBy)
       {
           using (var context = new CBContext())
           {
               var products = context.Products.ToList();
               if (CategoryID.HasValue)
               {
                  products =  products.Where(x => x.Category.ID == CategoryID.Value).ToList();
               }
               if (maximumPrice.HasValue && minimumPrice.HasValue)
               {
                   products = products.Where(x => x.Price >= minimumPrice && x.Price <= maximumPrice).OrderBy(x=>x.Price).ToList();
               }
               
               if (!string.IsNullOrEmpty(searchTerm))
               {
                  products =  products.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower())).ToList();
               }
               if(sortBy.HasValue)
               {
                   switch (sortBy)
                   {
                       case 2: products = products.OrderByDescending(x=>x.ID).ToList();
                           break;
                       case 3: products = products.OrderBy(x => x.Price).ToList();
                           break;
                       case 4: products = products.OrderByDescending(x => x.Price).ToList();
                           break;
                       default: products = products.OrderByDescending(x => x.ID).ToList();
                          break ;
                   }
               }
               return products;
           }
          
       }

       public decimal GetMaxPrice()
       {
           using (var context = new CBContext())
           {
               return context.Products.Max(x=>x.Price);

           }
       }

     
    }
}
