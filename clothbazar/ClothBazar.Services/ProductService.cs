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
            get
            {
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

        public List<Product> GetProduct(string search, int pageNo, int pageSize)
        {

            //var context = new CBContext();
            //return context.Products.Include(x => x.Category).ToList();
            using (var context = new CBContext())
            {
                var product = context.Products.Include(x => x.Category).ToList();
                if (string.IsNullOrEmpty(search) == false)
                {

                    product = context.Products.Where(x => x.Name != null && x.Name.ToLower().Contains(search.ToLower())).ToList();

                }
                product = product.OrderBy(x => x.ID).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
                return product;

            }
        }

        public List<Product> GetProduct(int pageNo)
        {
            int pageSize = 3;
            //var context = new CBContext();
            //return context.Products.Include(x => x.Category).ToList();
            using (var context = new CBContext())
            {
                return context.Products.OrderBy(x => x.ID).Skip((pageNo - 1) * pageSize).Take(pageSize).Include(x => x.Category).ToList();

            }
        }


        public List<Product> GetBestSaleProduct(int pageNo, int pageSize)
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
        public List<Product> GetRelatedProduct(int count, Category categ)
        {

            //var context = new CBContext();
            //return context.Products.Include(x => x.Category).ToList();
            using (var context = new CBContext())
            {
                return context.Products.Where(x => x.Category.Name == categ.Name).OrderByDescending(x => x.ID).Take(count).Include(x => x.Category).ToList();

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





        public List<Product> ProductsForShopPage(string searchTerm, int? maximumPrice, int? minimumPrice, int? CategoryID, int? sortBy,int pageNo,int pageSize)
        {
            using (var context = new CBContext())
            {
                var products = context.Products.ToList();
               
                if (CategoryID.HasValue)
                {
                    products = products.Where(x => x.Category.ID == CategoryID.Value).ToList();
                }
                if (maximumPrice.HasValue && minimumPrice.HasValue)
                {
                    products = products.Where(x => x.Price >= minimumPrice && x.Price <= maximumPrice).OrderBy(x => x.Price).ToList();
                }
                if (maximumPrice.HasValue && minimumPrice.HasValue && CategoryID.HasValue)
                {
                    products = products.Where(x => x.Price >= minimumPrice && x.Price <= maximumPrice && x.Category.ID == CategoryID).OrderBy(x => x.Price).ToList();
                }

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    products = products.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower())).ToList();
                }
                if (sortBy.HasValue)
                {
                    switch (sortBy)
                    {
                        case 2: products = products.OrderByDescending(x => x.ID).ToList();
                            break;
                        case 3: products = products.OrderBy(x => x.Price).ToList();
                            break;
                        case 4: products = products.OrderByDescending(x => x.Price).ToList();
                            break;
                        default: products = products.OrderByDescending(x => x.ID).ToList();
                            break;
                    }
                }
                
                
                    products = products.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
                
               
                return products;
            }

        }

        public decimal GetMaxPrice()
        {
            using (var context = new CBContext())
            {
                return context.Products.Max(x => x.Price);

            }
        }



        public int GetProductCount(string search, int? filteredminimumPrice, decimal? filteredmaximumPrice,int? categoryID)
        {
            using (var context = new CBContext())
            {
                int count = context.Products.Include(x => x.Category).Count();
                if (!string.IsNullOrEmpty(search))
                {
                    count = context.Products.Where(x => x.Name != null && x.Name.ToLower().Contains(search.ToLower())).ToList().Count();
                }

                if (categoryID.HasValue)
                {
                    count = context.Products.Where(x => x.Category.ID == categoryID.Value).ToList().Count();
                }
                if (filteredminimumPrice.HasValue && filteredmaximumPrice.HasValue)
                {

                      count = context.Products.Where(x => x.Price >= filteredminimumPrice && x.Price <= filteredmaximumPrice ).ToList().Count();
                }
                if (filteredminimumPrice.HasValue && filteredmaximumPrice.HasValue && categoryID.HasValue)
                {

                    count = context.Products.Where(x => x.Price >= filteredminimumPrice && x.Price <= filteredmaximumPrice && x.Category.ID == categoryID.Value).ToList().Count();
                }

                return count;

            }
        }

        public int GetProductCountSolely(string search)
        {
            using (var context = new CBContext())
            {
                int count = context.Products.Include(x => x.Category).Count();
                if (!string.IsNullOrEmpty(search))
                {
                    count = context.Products.Where(x => x.Name != null && x.Name.ToLower().Contains(search.ToLower())).ToList().Count();
                }
                return count;
            }
        }
    }
}