
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectWombat.Models;

namespace ProjectWombat.Contracts {
    public interface IOrderService {
        Task<Order> CreateOrder();
        Task<Order> GetOrder(string orderId);
        Task<IList<Order>> GetOrders();
        Task<Order> SaveOrder(Order order);
        Task UpdateOrderRow(string orderId, string productId, int count);
        Task UpdateOrderStatus(string orderId, OrderStatus orderStatus);
    }
}