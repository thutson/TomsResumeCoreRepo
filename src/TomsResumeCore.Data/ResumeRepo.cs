using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TomsResumeCore.Data.Interfaces;
using TomsResumeCore.DomainModels;

namespace TomsResumeCore.Data
{
    public class ResumeRepo : IResumeRepo
    {
        private readonly IConfiguration _config;

        public ResumeRepo(IConfiguration config)
        {
            _config = config;
        }

        public async Task<System.IO.Stream> GetResumePDF()
        {
            //The resume is saved as a PDF file and hosted on google drives.
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var GoogleDriveURL = _config["GoogleDrive:Url"];

                    if (GoogleDriveURL == null)
                        throw new ArgumentNullException("The GoogleDrive:Url param is missing from appsettings.json");

                    var GoogleDriveWorkHistoryFileId = _config["GoogleDrive:ResumeFileId"];

                    if (GoogleDriveWorkHistoryFileId == null)
                        throw new ArgumentNullException("The GoogleDrive:ResumeFileId param is missing from appsettings.json");

                    var Uri = new Uri(GoogleDriveURL + GoogleDriveWorkHistoryFileId);

                    HttpResponseMessage response = await client.GetAsync(Uri);
                    response.EnsureSuccessStatusCode();

                    return await response.Content.ReadAsStreamAsync();
                }
                catch (HttpRequestException ex)
                {
                    throw ex;
                }
            }
        }
        public string GetResumeFileName()
        {
            var ResumeFileName = _config["ResumeFileName"];

            if (ResumeFileName == null)
                throw new ArgumentNullException("The ResumeFileName param is missing from appsettings.json");

            return ResumeFileName;
        }
    }
}
