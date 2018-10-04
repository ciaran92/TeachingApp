using System.Threading.Tasks;

namespace TeachingAppAPI.Data
{
    public interface IEmailSender
    {

        Task SendEmailAsync(string key, string email, string subject, string message);

        Task Execute(string apiKey, string subject, string message, string email);
    }
}