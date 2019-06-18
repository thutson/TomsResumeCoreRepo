using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TomsResumeCore.Service
{
    public class VisitService : IVisitService
    {
        private readonly IConfiguration _config;

        public VisitService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SaveVisit(string IpAddress, string Page, string UserAgent)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var SecretVisitUrl = _config["SecretVisitUrl"];

                    if (SecretVisitUrl == null)
                        throw new ArgumentNullException("The SecretVisitUrl param is missing from appsettings.json");

                    var jsonObject = new
                    {
                        IpAddress = IpAddress,
                        Page = Page,
                        UserAgent = UserAgent
                    };

                    var content = new StringContent(JsonConvert.SerializeObject(jsonObject), Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(new Uri(SecretVisitUrl + "/Visits"), content);
                    response.EnsureSuccessStatusCode();
                }
                catch (HttpRequestException)
                {
                    //We want this to fail silently so we would do some logging here instead of throwing exceptions.
                }
                catch (Exception)
                {
                    //We want this to fail silently so we would do some logging here instead of throwing exceptions.
                }
            }
        }
    }
}
