
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ProjectWombat.Contracts;
using ProjectWombat.Models;

namespace ProjectWombat.Services {
    public class QrCodeGenerator : IQrCodeGenerator {
        private readonly ApplicationConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public QrCodeGenerator(IOptions<ApplicationConfiguration> configuration, IHttpClientFactory httpClientFactory) {
            _configuration = configuration.Value;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<byte[]> GetQrCodeForOrder(Order order) {
            var prefilled = new {
                format = "png",
                size = 600,
                payee = new {
                    value = _configuration.SwishReceiver,
                    editable = false
                },
                amount = new {
                    value = order.Amount,
                    editable = false
                },
                message = new {
                    value = order.Id,
                    editable = false
                }
            };

            var client = _httpClientFactory.CreateClient("swish-api");
            var response = await client.PostAsJsonAsync("prefilled", prefilled);
            var buffer = await response.Content.ReadAsByteArrayAsync();
            
            return buffer;
        }
    }
}