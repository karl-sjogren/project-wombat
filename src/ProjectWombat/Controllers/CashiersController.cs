using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectWombat.Contracts;
using ProjectWombat.Models;

namespace ProjectWombat.Controllers {
    [Route("api/cashiers")]
    public class CashiersController : Controller {
        private readonly ICashierService _cashierService;

        public CashiersController(ICashierService cashierService) {
            _cashierService = cashierService;
        }

        [HttpGet]
        public async Task<IEnumerable<Cashier>> GetCashiers() {
            return await _cashierService.GetCashiers();
        }

        [HttpGet("{cashierId}")]
        public async Task<Cashier> GetCashier(string cashierId) {
            return await _cashierService.GetCashier(cashierId);
        }

        [HttpPut("{cashierId}/{orderId}")]
        public async Task SetActiveOrder(string cashierId, string orderId) {
            await _cashierService.SetActiveOrder(cashierId, orderId);
        }
    }
}
