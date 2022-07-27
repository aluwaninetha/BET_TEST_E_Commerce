using E_Commerce.Application.Email;
using E_Commerce.Application.Exceptions;
using E_Commerce.Application.Interfaces;
using E_Commerce.Domain.Settings;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Text;

namespace E_Commerce.Infrastructure.Shared.Services
{
    public class EmailService : IEmailService
    {
        public MailSettings _mailSettings { get; }

        public EmailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;

        }

        public async Task SendAsync(EmailRequest request)
        {
            try
            {
                SmtpClient client = new SmtpClient();

                MailAddress from = new MailAddress(_mailSettings.DisplayName, request.From ?? _mailSettings.EmailFrom);

                MailAddress to = new MailAddress(request.To);

                MailMessage mailMessage = new MailMessage(from, to);

                mailMessage.Body = request.Body;
                mailMessage.BodyEncoding = Encoding.UTF8;
                mailMessage.IsBodyHtml = true;
                mailMessage.Subject = request.Subject;
                mailMessage.SubjectEncoding = Encoding.UTF8;

                await client.SendMailAsync(mailMessage);
                // _logger.LogInfo(string.Format("Notification sent to: {0}", reciever));
                mailMessage.Dispose();

            }
            catch (System.Exception ex)
            {
                //_logger.LogError(ex.Message, ex);
                throw new ApiException(ex.Message);
            }
        }
    }
}
