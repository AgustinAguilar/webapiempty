using Web.Api.Resources.BaseEmailTemplate;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Api.Repositories.IRepositories
{
    public interface IDeliveryNoteEmailSenderRepository
    {
        Task Send(List<string> to, string subject, string fileName, byte[] fileByteArray, BaseEmailTemplateFactory emailBody);
        Task SendNotification(List<string> to, string subject, string text);
        Task SendNotification(string to, string subject, string text);
        Task SendTestMail(string to);
    }
}
