
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using sendEmail.Model;
using sendEmail.Setting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace sendEmail.Service
{
    public class EmailService : IEmailService
    {
        private readonly MailSetting _mailSetting;
        public EmailService(IOptions<MailSetting> options)
        {
            _mailSetting = options.Value;
        }
        public async Task SendEmailAsync(MaileRequst maileRequst)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSetting.Mail);
            email.To.Add(MailboxAddress.Parse(maileRequst.ToEmail));
            email.Subject = maileRequst.Subject;
            var builder = new BodyBuilder();
            if (maileRequst.Attachment!=null)
            {
                byte[] fileBytes;
                foreach (var file in maileRequst.Attachment)
                {
                    if (file.Length>0)
                    {
                        using(var ms=new MemoryStream())
                        {
                            file.CopyTo(ms);
                            fileBytes = ms.ToArray();
                        }
                        builder.Attachments.Add(file.FileName,fileBytes,ContentType.Parse(file.ContentType));
                    }
                }
            }
            builder.HtmlBody = maileRequst.Body;
            email.Body = builder.ToMessageBody();
            using var smpt = new SmtpClient();
            smpt.Connect(_mailSetting.Host, _mailSetting.port, SecureSocketOptions.StartTls);
            smpt.Authenticate(_mailSetting.Mail, _mailSetting.Password);
            await smpt.SendAsync(email);
            smpt.Disconnect(true);
        }
    }
}
