using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using ProjectWombat.Contracts;
using ProjectWombat.Models;

namespace ProjectWombat.Services {
    public class ProductService : IProductService {
        private readonly ApplicationConfiguration _configuration;

        public ProductService(IOptions<ApplicationConfiguration> configuration) {
            _configuration = configuration.Value;
        }

        public Task<Product> GetProduct(string productId) {
            var product = _configuration.Products.FirstOrDefault(o => string.Equals(o.Id, productId, StringComparison.OrdinalIgnoreCase));
            return Task.FromResult(product);
        }

        public Task<IList<Product>> GetProducts() {
            IList<Product> products = _configuration.Products;
            return Task.FromResult(products);
        }
    }
}
