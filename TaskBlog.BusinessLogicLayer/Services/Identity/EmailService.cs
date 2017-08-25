using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace TaskBlog.BusinessLogicLayer.Services.Identity
{
    class EmailService : IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            using (var client = new SmtpClient())
            {
                client.Host = "mail.example.com";
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(@"noreply", "P@ssw0rd");

                var from = new MailAddress("no-reply@example.com");
                var to = new MailAddress(message.Destination);

                var mailMessage = new MailMessage(from, to)
                {
                    Subject = message.Subject,
                    Body = message.Body,
                    IsBodyHtml = true
                };
                await client.SendMailAsync(mailMessage);
            }
        }
    }
}
