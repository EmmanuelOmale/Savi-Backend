using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Savi.Core.Interfaces;
using Savi.Data.Domains;
using Savi.Data.Enums;

namespace Savi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public MailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        public async Task AddEmailTemplate([FromBody] EmailTemplate template)
        {
           await _emailService.AddEmailTemplate(template);
        }
    }
}
