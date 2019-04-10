using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using ProjectWombat.Contracts;
using ProjectWombat.Hubs;
using ProjectWombat.Models;

namespace ProjectWombat.Services {
    public class OrderService : IOrderService {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductService _productService;
        private readonly IHubContext<CashierHub> _cashierHub;

        public OrderService(IOrderRepository orderRepository, IProductService productService, IHubContext<CashierHub> cashierHub) {
            _orderRepository = orderRepository;
            _productService = productService;
            _cashierHub = cashierHub;
        }

        public async Task<Order> CreateOrder() {
            var order = await _orderRepository.CreateOrder();
            await _cashierHub.Clients.All.SendAsync("OrderCreated", order);
            return order;
        }

        public async Task<Order> GetOrder(string orderId) {
            var order = await _orderRepository.GetOrder(orderId);
            return order;
        }

        public async Task<IList<Order>> GetOrders() {
            var orders = await _orderRepository.GetOrders();
            return orders;
        }

        public async Task<Order> SaveOrder(Order order) {
            order = await _orderRepository.Save(order);
            await _cashierHub.Clients.All.SendAsync("OrderUpdated", order);
            return order;
        }

        public async Task UpdateOrderRow(string orderId, string productId, int count) {
            var order = await _orderRepository.GetOrder(orderId);
            var orderRow = order.Rows.FirstOrDefault(row => string.Equals(row.Product.Id, productId, StringComparison.OrdinalIgnoreCase));
            if(orderRow == null) {
                var product = await _productService.GetProduct(productId);
                orderRow = new OrderRow {
                    Product = product,
                    Count = count
                };
                order.Rows.Add(orderRow);
            } else {
                orderRow.Count = count;
                if(orderRow.Count == 0) {
                    order.Rows.Remove(orderRow);
                }
            }

            _ = await SaveOrder(order);
        }

        public async Task UpdateOrderStatus(string orderId, OrderStatus orderStatus) {
            var order = await _orderRepository.GetOrder(orderId);
            order.Status = orderStatus;

            _ = await SaveOrder(order);
        }
    }
}