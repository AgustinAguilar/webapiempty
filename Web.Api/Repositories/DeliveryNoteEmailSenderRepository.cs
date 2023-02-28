using Web.Api.Repositories.IRepositories;
using Web.Api.Resources;
using Web.Api.Resources.BaseEmailTemplate;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Web.Api.Utils.Smtp;
using Microsoft.Extensions.Options;
using MailKit.Net.Smtp;
using MimeKit;

namespace Web.Api.Repositories
{
    public class DeliveryNoteEmailSenderRepository : IDeliveryNoteEmailSenderRepository
    {
        private readonly Smtp SmtpInfo;

        public DeliveryNoteEmailSenderRepository(IOptions<Smtp> options)
        {
            SmtpInfo = options.Value;
        }
        public async Task Send(List<string> to, string subject, string fileName, byte[] fileByteArray, BaseEmailTemplateFactory emailBody)
        {
            var message = new MimeMessage();
            var toAddresses = to.Select(p => new MailboxAddress(p));
            var from = SmtpInfo.SenderEmail;
            var fromAddress = new MailboxAddress(from);
            message.From.Add(fromAddress);
            toAddresses.ToList().ForEach(p => message.To.Add(p));
            message.Subject = subject;

            var ms = new MemoryStream(fileByteArray);
            ms.Flush();
            var attachment = new MimePart(CodesStrings.CsvMediaType, CodesStrings.CsvMediaSubType)
            {
                Content = new MimeContent(ms, ContentEncoding.Default),
                ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                ContentTransferEncoding = ContentEncoding.Base64,
                FileName = fileName,
            };

            var builder = new BodyBuilder
            {
                HtmlBody = emailBody.GetContent()
            };

            var multipart = new Multipart("mixed")
            {
                builder.ToMessageBody(),
                attachment
            };

            message.Body = multipart;

            using (var smtpClient = new SmtpClient())
            {
                smtpClient.Connect(this.SmtpInfo.Server, this.SmtpInfo.Port, this.SmtpInfo.Security);
                smtpClient.Authenticate(this.SmtpInfo.SenderEmail, this.SmtpInfo.Password);
                await smtpClient.SendAsync(message);
                await smtpClient.DisconnectAsync(true);
            }
        }

        public async Task SendNotification(List<string> to, string subject, string text)
        {
            var message = new MimeMessage();
            var toAddresses = to.Select(p => new MailboxAddress(p));
            var from = SmtpInfo.SenderEmail;
            var fromAddress = new MailboxAddress(from);
            message.From.Add(fromAddress);
            toAddresses.ToList().ForEach(p => message.To.Add(p));
            message.Subject = subject;
            message.Body = new TextPart("html") { Text = text };

            using (var smtpClient = new SmtpClient())
            {
                smtpClient.Connect(SmtpInfo.Server, SmtpInfo.Port, SmtpInfo.Security);
                smtpClient.Authenticate(SmtpInfo.SenderEmail, SmtpInfo.Password);
                await smtpClient.SendAsync(message);
                await smtpClient.DisconnectAsync(true);
            }
        }

        public async Task SendNotification(string to, string subject, string text)
        {
            var message = new MimeMessage();
            message.To.Add(new MailboxAddress(to));
            var from = SmtpInfo.SenderEmail;
            var fromAddress = new MailboxAddress(from);
            message.From.Add(fromAddress);
            message.Subject = subject;
            message.Body = new TextPart("html") { Text = text };

            using (var smtpClient = new SmtpClient())
            {
                smtpClient.Connect(SmtpInfo.Server, SmtpInfo.Port, SmtpInfo.Security);
                smtpClient.Authenticate(SmtpInfo.SenderEmail, SmtpInfo.Password);
                await smtpClient.SendAsync(message);
                await smtpClient.DisconnectAsync(true);
            }
        }

        public async Task SendTestMail(string to)
        {
            var message = new MimeMessage();
            message.To.Add(new MailboxAddress(to));
            var from = this.SmtpInfo.SenderEmail;
            var fromAddress = new MailboxAddress(from);
            message.From.Add(fromAddress);
            message.Subject = Messages.TestEmailSubject;
            message.Body = new TextPart("html") { Text = $"{Messages.TestEmailMessage} {DateTime.Now}" };

            using (var smtpClient = new SmtpClient())
            {
                smtpClient.Connect(this.SmtpInfo.Server, this.SmtpInfo.Port, this.SmtpInfo.Security);
                smtpClient.Authenticate(this.SmtpInfo.SenderEmail, this.SmtpInfo.Password);
                await smtpClient.SendAsync(message);
                await smtpClient.DisconnectAsync(true);
            }
        }
    }
}
