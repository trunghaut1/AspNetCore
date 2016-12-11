using AspNetCore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Models
{
    public interface IProductRepository
    {
        IEnumerable<Product> Products { get; }
        IEnumerable<Product> GetByNumber(int number);
        Product GetById(int id);
        ProductListViewModel GetByCat(int id, int pageSize, int page);
        ProductListViewModel GetPage(int pageSize, int page);
        ProductListViewModel Search(string search, int pageSize, int page);
        bool SaveProduct(Product product);
        bool Delete(int id);
    }
}
