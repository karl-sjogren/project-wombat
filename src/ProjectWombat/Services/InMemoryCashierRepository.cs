using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using ProjectWombat.Contracts;
using ProjectWombat.Models;

namespace ProjectWombat.Services {
    public class InMemoryCashierRepository : ICashierRepository {
        private readonly ApplicationConfiguration _configuration;
        private readonly ConcurrentBag<Cashier> _cashiers;
        private readonly Random _random = new Random();

        public InMemoryCashierRepository(IOptions<ApplicationConfiguration> configuration) {
            _configuration = configuration.Value;
            _cashiers = new ConcurrentBag<Cashier>();

            foreach(var cashier in _configuration.Cashiers) {
                _cashiers.Add(new Cashier {
                    Id = cashier.Id,
                    Name = cashier.Name
                });
            }
        }

        public Task<Cashier> CreateCashier(string name) {
            var cashierId = string.Empty;
            do {
                cashierId = GenerateCashierId();
            } while(_cashiers.Any(o => string.Equals(o.Id, cashierId, StringComparison.OrdinalIgnoreCase)));

            var cashier = new Cashier {
                Id = cashierId,
                Name = name
            };

            return Task.FromResult(cashier);
        }

        public Task<Cashier> GetCashier(string cashierId) {
            var cashier = _cashiers.FirstOrDefault(o => string.Equals(o.Id, cashierId, StringComparison.OrdinalIgnoreCase));
            return Task.FromResult(cashier);
        }

        public Task<IList<Cashier>> GetCashiers() {
            IList<Cashier> cashiers = _cashiers.ToList();
            return Task.FromResult(cashiers);
        }

        public async Task<Cashier> SaveCashier(Cashier cashier) {
            var existingCashier = await GetCashier(cashier.Id);
            existingCashier.ActiveOrderId = cashier.ActiveOrderId;
            return existingCashier;
        }

        private string GenerateCashierId() {
            const string chars = "ABCDEFGHJKLMNPQRSTUVWXYZ0123456789";
            const Int32 length = 6;
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[_random.Next(s.Length)]).ToArray()).ToLowerInvariant();
        }
    }
}
