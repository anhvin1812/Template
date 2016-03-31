using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace App.Infrastructure.IdentityManagement
{
    public class IdentityEmail : IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            using (var mail = new MailMessage())
            {
                mail.From = new MailAddress(message.Destination);
                mail.To.Add(message.Destination);
                mail.Subject = message.Subject;
                mail.Body = message.Body;
                mail.IsBodyHtml = true; // Can set to false, if you are sending pure text.

                using (var smtp = new System.Net.Mail.SmtpClient())
                {
                    await smtp.SendMailAsync(mail);
                }
            }
        }
    }
}
