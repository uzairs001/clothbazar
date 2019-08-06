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
   public class CategoryService
    {
        public void SaveCategory(Category category)
        {
            using (var context = new CBContext())
            {
                context.Categories.Add(category);
                context.SaveChanges();
            }
            
           
        }

        public List<Category> GetCategory()
        {
            //var context = new CBContext();
            //return context.Categories.ToList();
            using (var context = new CBContext())
            {
                return context.Categories.Include(x => x.Products).ToList();

            }
            
        }

        public List<Category> GetFeaturedCategory()
        {
            using (var context = new CBContext())
            {
                return context.Categories.Where(x=>x.IsFeatured).ToList();

            }
        }
        public Category EditCategory(int ID)
        {
            using (var context = new CBContext())
            {
                return context.Categories.Find(ID);

            }
        }

        public void UpdateCategory(Category category)
        {
            using (var context = new CBContext())
            {
                context.Entry(category).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();

            }
        }

        public void DeleteCategory(int ID)
        {
            using (var context = new CBContext())
            {
               var category =  context.Categories.Find(ID);
               context.Categories.Remove(category);
                context.SaveChanges();
            }
        }

       
    }
}
