using BG.CampusLife.Domain.Enums;

namespace BG.CampusLife.Infrastructure
{
    public static class SendingEmail
    {
        //public string VerificationEmail()
        //{
        //    SmtpClient smtp = new SmtpClient
        //    {
        //        Host = "",
        //        EnableSsl = true,
        //        DeliveryMethod = SmtpDeliveryMethod.Network,
        //        //Port = 587,
        //        Credentials = new System.Net.NetworkCredential("", "")
        //    };

        //    StringBuilder template = new();
        //    template.AppendLine("Hi @Model.LastName");
        //    template.AppendLine("<p>Thanks for getting started with our website</p>");
        //    template.AppendLine("Click below to confirm your email address:");
        //    template.AppendLine("@Link");
        //    template.AppendLine("If you have any problem please contact @CompanySupportTeamEmailAddress.");
        //    template.AppendLine("We are glad you are here!");
        //    template.AppendLine("The CampusLife team");

        //    Email.DefaultSender = new SmtpSender(smtp);
        //    var email = Email
        //      .From("")
        //      .To("")
        //      .Subject("")
        //      .UsingTemplate(template.ToString(), new { LastName = "", Link = "", SuppertEmailAddress = ""});

        //    var result = email.Send();

        //    if (result.Successful)
        //        return "Email has been sent to your email account";

        //    return "Email could not be sent to your email. Please try again.";
        //}
        public static string CreateEmailBody(string header, string url, EmailTypes type)
        {
            string body = ConfirmRegisterEmailTemplate();

            body = body.Replace("{EmailHeader}", "Dear User " + header);

            if (type == EmailTypes.Register)
            {
                body = body.Replace("{EmailBody}", "Thanks for getting started with our website, Please click on the link below to confirm your account");
            }

            if (type == EmailTypes.ResetPassword)
                body = body.Replace("{EmailBody}", "Please click on the link below to recover your password");

            body = body.Replace("{EmailUrl}", url);
            body = body.Replace("{EmailFooter}", "Regards, Campus Life team");

            return body;
        }

        private static string ConfirmRegisterEmailTemplate()
        {
            string str = @"<!DOCTYPE html>
                            <html>
                            <head>
                                <meta charset='utf-8' />
                                <style>
                                    .email-body {
                                        border: 5px solid #ddd;
                                        direction: ltr;
                                        font-family: Tahoma;
                                        border-radius: 5px;
                                        padding: 20px;
                                        margin: 20px 0;
                                        background: #f9f9f9;
                                        margin-right: 10px;
                                    }
                            
                                        .email-body img {
                                            float: right;
                                            width: 180px;
                                        }
                            
                                    .email-footer {
                                        padding-top: 15px;
                                    }
                                </style>
                            </head>
                            <body dir='ltr'>
                                <div class='email-body'>
                                    <img src='CampusLifeLogoLink' />
                                    <div class='email-title'>{EmailHeader}</div>
                                    <p>{EmailBody}</p>
                                    <div><a href={EmailUrl}>Click Here</a></div>
                                    <!--<div>{UserCardInfo}</div>-->
                                    <div class='email-footer'>{EmailFooter}</div>
                                </div>
                            </body>
                            </html>";
            return str;
        }

    }

}
