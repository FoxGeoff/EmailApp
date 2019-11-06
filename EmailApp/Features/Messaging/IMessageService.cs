using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace WebUi.Features.Messaging
{
    public interface IMessageService
    {
        Task SendEmailAsync(
            string fromDisplayName,
            string fromEmailAddress,
            string toName,
            string toEmailAddress,
            string subject,
            string message,
            params Attachment[] attachments);

        Task SendEmailToSupportAsync(string subject, string message);
        Task SendExceptionEmailAsync(Exception e, HttpContext context);
    }
}
