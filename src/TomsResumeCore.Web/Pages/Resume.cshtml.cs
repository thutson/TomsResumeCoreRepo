using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Net.Http.Headers;
using TomsResumeCore.Data.Interfaces;
using TomsResumeCore.Service;

namespace TomsResumeCore.Web.Pages
{
    public class ResumeModel : PageModel
    {
        private readonly IResumeRepo _resumeRepo;
        private readonly IVisitService _visitService;

        public ResumeModel(IResumeRepo resumeRepo, IVisitService visitService)
        {
            _resumeRepo = resumeRepo;
            _visitService = visitService;
        }

        public async Task<IActionResult> OnGet(bool download = false)
        {

            var IpAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            var UserAgent = Request.Headers["User-Agent"].ToString();

            if (!String.IsNullOrWhiteSpace(IpAddress) || !String.IsNullOrWhiteSpace(UserAgent))
            {
                await _visitService.SaveVisit(IpAddress, download ? "Resume Download" : "Resume View", UserAgent);
            }

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