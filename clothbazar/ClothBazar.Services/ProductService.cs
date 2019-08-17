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
    }
}
