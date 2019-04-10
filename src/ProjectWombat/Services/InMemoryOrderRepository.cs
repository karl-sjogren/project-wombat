using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using ProjectWombat.Contracts;
using ProjectWombat.Models;

namespace ProjectWombat.Services {
    public class InMemoryOrderRepository : IOrderRepository {
        private readonly ApplicationConfiguration _configuration;
        private readonly ConcurrentBag<Order> _orders;
        private readonly Random _random = new Random();

        public InMemoryOrderRepository(IOptions<ApplicationConfiguration> configuration) {
            _configuration = configuration.Value;
            _orders = new ConcurrentBag<Order>();
        }

        public Task<Order> CreateOrder() {
            var orderId = string.Empty;
            do {
                orderId = GenerateOrderId();
            } while(_orders.Any(o => string.Equals(o.Id, orderId, StringComparison.OrdinalIgnoreCase)));

            var order = new Order {
                Id = orderId
            };

            this._orders.Add(order);

            return Task.FromResult(order);
        }

        public Task<Order> GetOrder(string orderId) {
            var order = _orders.FirstOrDefault(o => string.Equals(o.Id, orderId, StringComparison.OrdinalIgnoreCase));
            return Task.FromResult(order);
        }

        public Task<IList<Order>> GetOrders() {
            IList<Order> orders = _orders.ToList();
            return Task.FromResult(orders);
        }

        public async Task<Order> Save(Order order) {
            var existingOrder = await GetOrder(order.Id);
            existingOrder.Amount = order.Amount;
            return existingOrder;
        }

        private string GenerateOrderId() {
            const string chars = "ABCDEFGHJKLMNPQRSTUVWXYZ0123456789";
            const Int32 length = 6;
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[_random.Next(s.Length)]).ToArray()).ToLowerInvariant();
        }
    }
}
