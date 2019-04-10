
using System.Threading.Tasks;
using ProjectWombat.Models;

public interface IQrCodeGenerator{
    Task<byte[]> GetQrCodeForOrder(Order order);
}