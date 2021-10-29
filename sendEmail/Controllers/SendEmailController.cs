using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sendEmail.Model;
using sendEmail.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sendEmail.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendEmailController : ControllerBase
    {
        private readonly IEmailService _eImailService;
        public SendEmailController(IEmailService eImailService)
        {
            _eImailService = eImailService;
        }
        [HttpPost("Send")]
        public async Task<IActionResult> Send([FromForm] MaileRequst mailRequest)
        {
            try
            {
                await _eImailService.SendEmailAsync(mailRequest);
                return Ok();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
