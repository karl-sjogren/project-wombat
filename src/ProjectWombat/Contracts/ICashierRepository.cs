using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectWombat.Models;

namespace ProjectWombat.Contracts {
    public interface ICashierRepository {
        Task<Cashier> CreateCashier(string name);
        Task<Cashier> GetCashier(string cashierId);
        Task<IList<Cashier>> GetCashiers();
        Task<Cashier> SaveCashier(Cashier cashier);
    }
}