using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectWombat.Models;

namespace ProjectWombat.Contracts {
    public interface IOrderRepository {
        Task<Order> CreateOrder();
        Task<Order> GetOrder(string orderId);
        Task<IList<Order>> GetOrders();
        Task<Order> Save(Order order);
    }
}