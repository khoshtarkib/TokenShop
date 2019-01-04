using System.Net.Mail;
using TokenShop.Models;

namespace TokenShop.Common
{
    public class MyMail
    {
        public static void Send(string subject,string Message,string To,Setting settings)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(settings.SmtpAddress, settings.SmtpPort);
                System.Net.NetworkCredential cred = new System.Net.NetworkCredential(settings.SmtpUserName, settings.SmtpPassword);
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = cred;
                mail.From = new MailAddress(settings.SmtpUserName);
                mail.To.Add(To);
                mail.Subject = subject;
                mail.Body = Message;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;
                SmtpServer.Send(mail);
            }
            catch
            {
            }
        }
    }
}