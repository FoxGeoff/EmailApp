using System;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using MimeKit;

using WebUi.Features.Messaging.Email;
using WebUi.Infrastructure;

namespace WebUi.Features.Messaging
{
    public class MessageService : IMessageService
    {
        private readonly IViewRenderer _viewRenderer;

        public MessageService(IViewRenderer viewRenderer)
        {
            _viewRenderer = viewRenderer;
        }

        public async Task SendEmailAsync(
            string fromDisplayName,
            string fromEmailAddress,
            string toName,
            string toEmailAddress,
            string subject,
            string message,
            params Attachment[] attachments)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(fromDisplayName, fromEmailAddress));
            email.To.Add(new MailboxAddress(toName, toEmailAddress));
            email.Subject = subject;

            var body = new BodyBuilder
            {
                HtmlBody = message
            };
            foreach (var attachment in attachments)
            {
                using (var stream = await attachment.ContentToStreamAsync())
                {
                    body.Attachments.Add(attachment.FileName, stream);
                }
            }

            email.Body = body.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;
                client.AuthenticationMechanisms.Remove("XOAUTH2");

                // Start of provider specific settings
                await client.ConnectAsync("smtp.host", 587, false).ConfigureAwait(false);
                await client.AuthenticateAsync("username", "password").ConfigureAwait(false);
                // End of provider specific settings

                await client.SendAsync(email).ConfigureAwait(false);
                await client.DisconnectAsync(true).ConfigureAwait(false);
            }
        }

        public async Task SendEmailToSupportAsync(string subject, string message)
        {
            await SendEmailAsync("No Reply", "no-reply@yourdomain.com", "Support", "support@yourdomain.com", subject, message);
        }

        public async Task SendExceptionEmailAsync(Exception e, HttpContext context)
        {
            //Orginal: "Features/Messaging/Email/ExceptionEmail" =>Error: Couldn't find view 'Features/Messaging/Email/ExceptionEmail'
            var message = _viewRenderer.Render("Features/Email/ExceptionEmail", new ExceptionEmailModel(e, context));

            await SendEmailToSupportAsync("Exception", message);
        }
    }
}
