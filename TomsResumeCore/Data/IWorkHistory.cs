using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomsResumeCore.Models;

namespace TomsResumeCore.Data
{
    public interface IWorkHistory
    {
        Task<List<JobHistory>> GetWorkHistoryAsync();
    }
}
