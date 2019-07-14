﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothBazar.Database;
using ClothBazar.Entity;

namespace ClothBazar.Services
{
   public class ProductService
    {
       public void SaveProduct(Product product)
        {
            using (var context = new CBContext())
            {
                context.Products.Add(product);
                context.SaveChanges();
            }
        }

       public List<Product> GetProduct()
        {
            using (var context = new CBContext())
            {
                return context.Products.ToList();
                
            }
        }

       public Product EditProduct(int ID)
        {
            using (var context = new CBContext())
            {
                return context.Products.Find(ID);

            }
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