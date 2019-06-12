using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TomsResumeCore.Service
{
    public interface IVisitService
    {
        Task SaveVisit(string IpAddress, string UserAgent);
    }
}
