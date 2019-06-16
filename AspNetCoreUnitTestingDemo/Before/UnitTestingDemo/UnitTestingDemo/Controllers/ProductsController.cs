using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UnitTestingDemo.Models;
using UnitTestingDemo.Services;

namespace UnitTestingDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            return _productService.GetProducts();
        }
    }
}
