using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectWombat.Contracts;
using ProjectWombat.Models;

namespace ProjectWombat.Controllers {
    [Route("api/products")]
    public class ProductsController : Controller {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService) {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> GetProducts() {
            return await _productService.GetProducts();
        }
    }
}
