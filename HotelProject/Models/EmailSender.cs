using HotelProject.Interfaces;
using System.Net;
using System.Net.Mail;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System.IO;
namespace HotelProject.Models
{
    public class EmailSender: IEmailSender
    {
        public Task SendEmailWithAttachmentAsync(string email, string subject, string message, Attachment attachment)
        {
            var mail = "Ta7alf@hotmail.com";
            var pw = "wassem48";

            var client = new SmtpClient("smtp-mail.outlook.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(mail, pw)
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(mail),
                Subject = subject,
                Body = message,
                IsBodyHtml = false,
            };

            mailMessage.To.Add(email);
            mailMessage.Attachments.Add(attachment);

            return client.SendMailAsync(mailMessage);
        }

        public async Task SendEmailAsync(string fromEmail, string toEmail, string subject, string message)
        {
            using (var smtpClient = new SmtpClient("smtp.your-email-provider.com"))
            {
                smtpClient.Port = 587;
                smtpClient.Credentials = new NetworkCredential("wassem305@hotmail.com", "Mos1960@Mos1960@Wassem48");
                smtpClient.EnableSsl = true;

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(fromEmail),
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = true,
                };

                mailMessage.To.Add(toEmail);

                await smtpClient.SendMailAsync(mailMessage);
            }
        }
    }
}

