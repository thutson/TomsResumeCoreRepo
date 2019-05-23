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
        const string GoogleDriveURL = @"https://drive.google.com/uc?export=view&id=";

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
                    var GoogleDriveFileId = _config["WorkHistoryGoogleDriveFileId"];

                    if (GoogleDriveFileId == null)
                        throw new ArgumentNullException("The WorkHistoryGoogleDriveFileId param is missing from appsettings.json");

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
