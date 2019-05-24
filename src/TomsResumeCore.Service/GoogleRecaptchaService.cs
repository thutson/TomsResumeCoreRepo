using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TomsResumeCore.Service
{
    public class GoogleRecaptchaService : IGoogleRecaptchaService
    {
        private readonly IConfiguration _config;

        public GoogleRecaptchaService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<bool> IsReCaptchaPassed(string recaptchaResponse)
        {
            if (String.IsNullOrWhiteSpace(recaptchaResponse))
                return false;

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var GoogleRecaptchaSecret = _config["GoogleReCaptcha:secret"];

                    if(GoogleRecaptchaSecret == null)
                        throw new ArgumentNullException("The GoogleReCaptcha:secret param is missing from appsettings.json");

                    var GoogleRecaptchaVerifyUrl = _config["GoogleReCaptcha:verifyurl"];

                    if (GoogleRecaptchaVerifyUrl == null)
                        throw new ArgumentNullException("The GoogleReCaptcha:verifyurl param is missing from appsettings.json");

                    var content = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("secret", _config["GoogleReCaptcha:secret"]),
                        new KeyValuePair<string, string>("response", recaptchaResponse)
                    });

                    HttpResponseMessage response = await client.PostAsync(new Uri(GoogleRecaptchaVerifyUrl), content);
                    response.EnsureSuccessStatusCode();
                    var responseBody = await response.Content.ReadAsStringAsync();
                    dynamic responseJson = JsonConvert.DeserializeObject<object>(responseBody);

                    if(responseJson.success == "true")
                        return true;

                    return false;

                }
                catch (HttpRequestException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
