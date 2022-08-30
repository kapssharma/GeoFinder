using GeoFinder.Utility.Services.Interface;

using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace GeoFinder.Utility.Services.Implementation
{
    public class EmailService : IEmailService
    {
        public async Task SendEmailAsync(IConfiguration configuration, string receiverEmail, string body, string Subject)
        {
            try
            {
                string mail = configuration.GetSection("MailSettings")["Mail"];
                string password = configuration.GetSection("MailSettings")["Password"];
                int port = Convert.ToInt16(configuration.GetSection("MailSettings")["Port"]);
                string host = configuration.GetSection("MailSettings")["Host"];
                string displayName = configuration.GetSection("MailSettings")["DisplayNames"];

                SmtpClient smtpClient = new SmtpClient(host, port);
                smtpClient.Credentials = new System.Net.NetworkCredential(mail, password);
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                MailMessage _mail = new MailMessage();
                _mail.Subject = Subject;
                _mail.Body = body;
                _mail.IsBodyHtml = true;
                _mail.From = new MailAddress(mail, displayName);
                _mail.To.Add(new MailAddress(receiverEmail));
                //mail.Bcc.Add(new MailAddress(""));
                smtpClient.EnableSsl = true;
                smtpClient.Send(_mail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
