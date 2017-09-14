using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using App.Core.Common;
using App.Core.Configuration;
using App.Core.Email;

namespace App.Infrastructure.Email
{
    public class MailSender : DisposableObject
    {
        public static void SendAsync(Mail mail)
        {

            using (var mailMessage = new MailMessage())
            {
                var smtp = AppSettings.ConfigurationProvider.SmtpConfiguration;

                

                mailMessage.From = new MailAddress(smtp.From, "NgheTinh24h");
                foreach (var emailTo in mail.EmailsTo)
                {
                    mailMessage.To.Add(emailTo);
                }

                mailMessage.Subject = mail.Subject;
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = mail.Body;

                var client = new SmtpClient(smtp.Network.Host, smtp.Network.Port);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.EnableSsl = true;
                client.Credentials = new System.Net.NetworkCredential(smtp.Network.UserName, smtp.Network.Password);

                client.Send(mailMessage);

                ////event handler for asynchronous call
                //Object state = mail;
                //client.SendCompleted += new SendCompletedEventHandler(SmtpClient_SendCompleted);
                //try
                //{
                   
                //    //client.SendAsync(mailMessage);
                //}
                //catch (Exception ex)
                //{
                //    /* exception handling code here */
                //}
            }

        }

        static void SmtpClient_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            var mail = e.UserState as MailMessage;

            if (!e.Cancelled && e.Error != null)
            {
                Debug.Write("Mail sent successfully");
            }
            else
            {
                Debug.Write(e.Error);
            }
        }
    }
}
