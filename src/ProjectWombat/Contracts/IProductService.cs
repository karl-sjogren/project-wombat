
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectWombat.Models;

namespace ProjectWombat.Contracts {
    public interface IProductService {
        Task<Product> GetProduct(string productId);
        Task<IList<Product>> GetProducts();
    }
}
