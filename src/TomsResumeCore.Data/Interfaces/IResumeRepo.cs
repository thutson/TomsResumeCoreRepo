using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TomsResumeCore.Data.Interfaces
{
    public interface IResumeRepo
    {
        Task<System.IO.Stream> GetResumePDF();

        string GetResumeFileName();
    }
}
