using AspNetCore.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Models
{
    public class EFProductRepository : IProductRepository
    {
        private DatabaseContext db;

        public EFProductRepository(DatabaseContext db)
        {
            this.db = db;
        }
        public IEnumerable<Product> Products => db.Product;
        public IEnumerable<Product> GetByNumber(int number)
        {
            return db.Product.Take(number).AsEnumerable();
        }
        public Product GetById(int id)
        {
            return db.Product.Include(o => o.Cat).FirstOrDefault(o => o.Id == id);
        }
        public ProductListViewModel GetByCat(int id, int pageSize, int page)
        {
            IEnumerable<Product> pro =  db.Product.Where(o => o.CatId == id).OrderBy(o => o.Id)
                .Skip((page - 1) * pageSize).Take(pageSize);
            return new ProductListViewModel
            {
                Products = pro,
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = db.Product.Where(o => o.CatId == id).Count()
                }
            };
        }
        public ProductListViewModel GetPage(int pageSize, int page)
        {
            IEnumerable<Product> pro = db.Product.OrderBy(o => o.Id)
                .Skip((page - 1) * pageSize).Take(pageSize);
            return new ProductListViewModel
            {
                Products = pro,
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = db.Product.Count()
                }
            };
        }
        public ProductListViewModel Search(string search, int pageSize, int page)
        {
            IEnumerable<Product> pro = db.Product.Where(o => o.Name.Contains(search)).OrderBy(o => o.Id)
                .Skip((page - 1) * pageSize).Take(pageSize);
            return new ProductListViewModel
            {
                Products = pro,
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = db.Product.Where(o => o.Name.Contains(search)).Count()
                }
            };
        }
        public bool SaveProduct(Product product)
        {
            try
            {
                if(product.Id > 0)
                {
                    db.Product.Update(product);
                }
                else
                {
                    db.Product.Add(product);
                }
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Delete(int id)
        {
            try
            {
                Product product = db.Product.Find(id);
                db.Product.Remove(product);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public void MinusQuantity(int id, int quantity)
        {
            Product value = db.Product.Find(id);
            if (value.Quantity > 0)
            {
                value.Quantity -= quantity;
                if (value.Quantity < 0)
                    value.Quantity = 0;
            }
            db.SaveChanges();
        }
    }
}
