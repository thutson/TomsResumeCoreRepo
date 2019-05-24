using System;
using System.Collections.Generic;
using System.Text;

namespace TomsResumeCore.DomainModels
{
    public class ContactPayload
    {
        public string name { get; set; }
        public string email { get; set; }
        public string message { get; set; }
    }
}
