using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectWombat.Contracts;
using ProjectWombat.Models;

namespace ProjectWombat.Controllers {
    [Route("api/orders")]
    public class OrdersController : Controller {
        private readonly IOrderService _orderService;
        private readonly IQrCodeGenerator _qrCodeGenerator;

        public OrdersController(IOrderService orderService, IQrCodeGenerator qrCodeGenerator) {
            _orderService = orderService;
            _qrCodeGenerator = qrCodeGenerator;
        }

        [HttpGet("{orderId}")]
        public async Task<Order> GetOrder(string orderId) {
            return await _orderService.GetOrder(orderId);
        }

        [HttpPost]
        public async Task<Order> CreateOrder() {
            var order = await _orderService.CreateOrder();
            return order;
        }

        [HttpPut("{orderId}")]
        public async Task UpdateOrderStatus(string orderId, [FromForm] OrderStatus status) {
            await _orderService.UpdateOrderStatus(orderId, status);
        }

        [HttpGet("{orderId}/swish-qr-code")]
        public async Task<IActionResult> SwishQrCode(string orderId) {
            var order = new Order {
                Id = orderId,
                Amount = 124
            };
            
            var buffer = await _qrCodeGenerator.GetQrCodeForOrder(order);

            return File(buffer, "image/png");
        }
    }
}
