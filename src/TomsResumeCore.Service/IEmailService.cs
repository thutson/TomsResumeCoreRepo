using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TomsResumeCore.Service
{
    public interface IEmailService
    {
        Task SendContactMessage(string name, string email, string message);
    }
}
