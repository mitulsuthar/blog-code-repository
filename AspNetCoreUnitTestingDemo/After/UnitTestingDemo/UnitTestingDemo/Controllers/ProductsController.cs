using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UnitTestingDemo.Models;
using UnitTestingDemo.Services;

namespace UnitTestingDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMyLogger _logger;

        public ProductsController(IProductService productService, IMyLogger logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [HttpGet]        
        [ProducesResponseType(typeof(List<Product>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]        
        public ActionResult Get()
        {
            try
            {
                var products = _productService.GetProducts();
                if (products?.Count > 0)
                {
                    return Ok(products);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.Log($"The method {nameof(ProductService.GetProducts)} caused an exception", ex);
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
