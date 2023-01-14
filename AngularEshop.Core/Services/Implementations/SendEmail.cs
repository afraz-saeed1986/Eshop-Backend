using AngularEshop.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace AngularEshop.Core.Services.Implementations
{
    public class SendEmail : IMailSender
    {
        //private ISiteService _siteService;

        //public SendEmail(ISiteService siteService)
        //{
        //    _siteService = siteService;
        //}

        public void Send(string to, string subject, string body)
        {
            try { 
            var defaultEmail = "sa.padnick@aol.com";
            var mail = new MailMessage();
            var smtpServer = new SmtpClient("esmtp.aol.com");
            mail.From = new MailAddress(defaultEmail, "فروشگاه انگولار");
            mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml= true;

            //System.Net.Mail.Attachment attachment;
            //attachment = new System.Net.Mail.Attachment("c:/textfile.txt");
            //mail.Attachments.Add(attachment);

            smtpServer.Port = 587;
            smtpServer.Credentials = new System.Net.NetworkCredential(defaultEmail, "S@a1711365118");

            smtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;

            smtpServer.EnableSsl = true;
            smtpServer.Send(mail);
            }
            catch(Exception ex) {
                throw ex;
            }
        }
    }
}
