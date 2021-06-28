using CheckoutSys.Application.DTOs.Mail;
using System.Threading.Tasks;

namespace CheckoutSys.Application.Interfaces.Shared
{
    public interface IMailService
    {
        Task SendAsync(MailRequest request);
    }
}