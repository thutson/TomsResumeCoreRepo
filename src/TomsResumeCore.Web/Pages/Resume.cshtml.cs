using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Net.Http.Headers;
using TomsResumeCore.Data.Interfaces;

namespace TomsResumeCore.Web.Pages
{
    public class ResumeModel : PageModel
    {
        private readonly IResumeRepo _resumeRepo;

        public ResumeModel(IResumeRepo resumeRepo)
        {
            _resumeRepo = resumeRepo;
        }

        public async Task<IActionResult> OnGet(bool download = false)
        {
            var fileStream = await _resumeRepo.GetResumePDF();

            Response.Headers.Add("x-robots-tag", "noindex");

            if (download)
                return new FileStreamResult(fileStream, new MediaTypeHeaderValue("application/octet-stream"))
                {
                    FileDownloadName = _resumeRepo.GetResumeFileName() + ".pdf"
                };
            else
                return new FileStreamResult(fileStream, new MediaTypeHeaderValue("application/pdf"));
        }
    }
}