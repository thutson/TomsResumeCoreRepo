using System;
using System.Collections.Generic;
using System.Text;

namespace TomsResumeCore.DomainModels
{
    public class SmtpSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public bool IsSSL { get; set; }
        public string SenderEmail { get; set; }
        public string SenderName { get; set; }
        public string Password { get; set; }
    }
}
