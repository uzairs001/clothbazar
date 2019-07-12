using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothBazar.Database;
using ClothBazar.Entity;

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
            using (var context = new CBContext())
            {
                return context.Categories.ToList();
                
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
