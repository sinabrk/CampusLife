using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace BG.CampusLife.Infrastructure
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Execute(subject, message, email);
        }

        public Task Execute(string subject, string message, string email)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("omid.ryuk@gmail.com", "0890519528"),
                //UseDefaultCredentials = false,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
            };
            return client.SendMailAsync(
                new MailMessage(new MailAddress("", ""), new MailAddress(email, email))
                {
                    IsBodyHtml = true,
                    Subject = subject,
                    Body = message
                }
            );
        }
    }
}
