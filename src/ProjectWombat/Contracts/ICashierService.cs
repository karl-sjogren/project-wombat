
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectWombat.Models;

namespace ProjectWombat.Contracts {
    public interface ICashierService {
        Task<Cashier> GetCashier(string cashierId);
        Task<IList<Cashier>> GetCashiers();
        Task SetActiveOrder(string cashierId, string orderId);
    }
}
