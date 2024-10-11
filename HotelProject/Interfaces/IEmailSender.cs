using System.Net.Mail;

namespace HotelProject.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string fromEmail,string email, string subject, string message);
        Task SendEmailWithAttachmentAsync(string email, string subject, string message, Attachment attachment);
    }
}
