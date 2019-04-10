using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using ProjectWombat.Contracts;

namespace ProjectWombat.Hubs {
    public class CashierHub : Hub {
        private readonly IOrderService _orderService;
        private readonly ICashierService _cashierService;

        public CashierHub(IOrderService orderService, ICashierService cashierService) {
            _orderService = orderService;
            _cashierService = cashierService;
        }

        public async Task JoinCashierGroup(string cashierId) {
            var cashier = await _cashierService.GetCashier(cashierId);
            if(cashier == null)
                throw new Exception("Couldn't find any cashier with id " + cashierId);

            await Groups.AddToGroupAsync(Context.ConnectionId, cashierId);
        }

        public async Task LeaveCashierGroup(string cashierId) {
            var cashier = await _cashierService.GetCashier(cashierId);
            if(cashier == null)
                throw new Exception("Couldn't find any cashier with id " + cashierId);

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, cashierId);
        }

        public async Task UpdateOrderRow(string orderId, string productId, Int32 count) {
            await _orderService.UpdateOrderRow(orderId, productId, count);
        }
    }
}