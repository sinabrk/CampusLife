using FluentEmail.Core;
using FluentEmail.Smtp;
using System.Net.Mail;
using System.Text;

namespace BG.CampusLife.Application.Helper
{
    public class SendingEmail
    {
        public string VerificationEmail()
        {
            SmtpClient smtp = new SmtpClient
            {
                Host = "",
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                //Port = 587,
                Credentials = new System.Net.NetworkCredential("", "")
            };

            StringBuilder template = new();
            template.AppendLine("Hi @Model.LastName");
            template.AppendLine("<p>Thanks for getting started with our website</p>");
            template.AppendLine("Click below to confirm your email address:");
            template.AppendLine("@Link");
            template.AppendLine("If you have any problem please contact @CompanySupportTeamEmailAddress.");
            template.AppendLine("We are glad you are here!");
            template.AppendLine("The CampusLife team");

            Email.DefaultSender = new SmtpSender(smtp);
            var email = Email
              .From("")
              .To("")
              .Subject("")
              .UsingTemplate(template.ToString(), new { LastName = "", Link = "", SuppertEmailAddress = ""});

            var result = email.Send();

            if (result.Successful)
                return "Email has been sent to your email account";

            return "Email could not be sent to your email. Please try again.";
        }
    }

}
