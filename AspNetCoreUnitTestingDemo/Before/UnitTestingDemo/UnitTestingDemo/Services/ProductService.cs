using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestingDemo.Models;

namespace UnitTestingDemo.Services
{
    public interface IProductService
    {
        List<Product> GetProducts();
    }

    public class ProductService : IProductService
    {
        public List<Product> GetProducts()
        {
            var products = new List<Product>();
            products.Add(new Product() { Name = "P1", Color = "C1", Price = "3.0" });
            products.Add(new Product() { Name = "P2", Color = "C2", Price = "4.0" });
            products.Add(new Product() { Name = "P3", Color = "C3", Price = "5.0" });
            products.Add(new Product() { Name = "P4", Color = "C4", Price = "6.0" });
            return products;
        }
    }
}
