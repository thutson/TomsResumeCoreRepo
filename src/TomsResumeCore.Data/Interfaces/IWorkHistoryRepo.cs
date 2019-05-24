using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TomsResumeCore.DomainModels;

namespace TomsResumeCore.Data
{
    public interface IWorkHistoryRepo
    {
        Task<List<JobHeld>> GetWorkHistoryAsync();
    }
}
