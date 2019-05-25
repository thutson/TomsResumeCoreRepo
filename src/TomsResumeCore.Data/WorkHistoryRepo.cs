using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TomsResumeCore.DomainModels;
using TomsResumeCore.Data;

namespace TomsResumeCore.Data
{
    public class WorkHistoryRepo : IWorkHistoryRepo
    {
        private readonly IConfiguration _config;

        public WorkHistoryRepo(IConfiguration config)
        {
            _config = config;
        }
        public async Task<List<JobHeld>> GetWorkHistoryAsync()
        {
            //Data is saved on a json file hosted on google drives.
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var GoogleDriveURL = _config["GoogleDrive:Url"];

                    if (GoogleDriveURL == null)
                        throw new ArgumentNullException("The GoogleDrive:Url param is missing from appsettings.json");

                    var GoogleDriveFileId = _config["GoogleDrive:FileId"];

                    if (GoogleDriveFileId == null)
                        throw new ArgumentNullException("The GoogleDrive:FileId param is missing from appsettings.json");

                    var Uri = new Uri(GoogleDriveURL + GoogleDriveFileId);

                    HttpResponseMessage response = await client.GetAsync(Uri);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<List<JobHeld>>(responseBody);
                }
                catch (HttpRequestException ex)
                {
                    throw ex;
                }
            }
        }
    }
}
