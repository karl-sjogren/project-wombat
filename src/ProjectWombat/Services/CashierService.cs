using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using ProjectWombat.Contracts;
using ProjectWombat.Hubs;
using ProjectWombat.Models;

namespace ProjectWombat.Services {
    public class CashierService : ICashierService {
        private readonly ICashierRepository _cashierRepository;
        private readonly IHubContext<CashierHub> _cashierHub;

        public CashierService(ICashierRepository cashierRepository, IHubContext<CashierHub> cashierHub) {
            _cashierRepository = cashierRepository;
            _cashierHub = cashierHub;
        }

        public async Task<Cashier> GetCashier(string cashierId) {
            return await _cashierRepository.GetCashier(cashierId);
        }

        public async Task<IList<Cashier>> GetCashiers() {
            return await _cashierRepository.GetCashiers();
        }

        public async Task SetActiveOrder(string cashierId, string orderId) {
            var cashier = await _cashierRepository.GetCashier(cashierId);
            cashier.ActiveOrderId = orderId;
            await _cashierRepository.SaveCashier(cashier);
            await _cashierHub.Clients.Group(cashierId).SendAsync("OrderActivated", orderId);
        }
    }
}
