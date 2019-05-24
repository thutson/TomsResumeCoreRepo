using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TomsResumeCore.DomainModels;
using TomsResumeCore.Service;

namespace TomsResumeCore.Web.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IEmailService _emailService;
        private readonly IGoogleRecaptchaService _gCaptchaService;

        public ContactController(IEmailService emailService, IGoogleRecaptchaService gCaptchaService)
        {
            _emailService = emailService;
            _gCaptchaService = gCaptchaService;
        }
        // GET: api/Contact
        [HttpGet]
        public IEnumerable<string> Get()
        {
            throw new NotImplementedException();
        }

        // GET: api/Contact/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            throw new NotImplementedException();
        }

        // POST: api/Contact
        [HttpPost]
        public async Task PostAsync([FromForm] ContactPayload payload)
        {
            if(
                !String.IsNullOrWhiteSpace(payload.name) &&
                !String.IsNullOrWhiteSpace(payload.email) &&
                !String.IsNullOrWhiteSpace(payload.message) &&
                !String.IsNullOrWhiteSpace(payload.recaptcha)
            )
            await _emailService.SendContactMessage(payload.name, payload.email, payload.message, payload.recaptcha);
        }

        // PUT: api/Contact/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            throw new NotImplementedException();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
