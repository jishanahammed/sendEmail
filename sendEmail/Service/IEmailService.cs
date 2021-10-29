using sendEmail.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sendEmail.Service
{
  public  interface IEmailService
    {
        Task SendEmailAsync(MaileRequst maileRequst);
    }
}
