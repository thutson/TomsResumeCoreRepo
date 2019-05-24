using System;
using System.Collections.Generic;
using System.Net.Cache;
using System.Threading.Tasks;

namespace TomsResumeCore.Service
{
    public class EmailService : IEmailService
    {
        public async Task SendContactMessage(string name, string email, string message)
        {
            var test = 0;
        }
    }
}
