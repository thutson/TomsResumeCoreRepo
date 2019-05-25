using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using TomsResumeCore.Data;
using TomsResumeCore.DomainModels;
using TomsResumeCore.Service;

namespace TomsResumeCore.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                //options.CheckConsentNeeded = context => true;
                //options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.Configure<SmtpSettings>(values =>
            {
                values.Host = Configuration["smtp:Host"];
                values.Port = Convert.ToInt32(Configuration["smtp:Port"]);
                values.IsSSL = Convert.ToBoolean(Configuration["smtp:IsSSL"]);
                values.SenderEmail = Configuration["smtp:SenderEmail"];
                values.SenderName = Configuration["smtp:SenderName"];
                values.Password = Configuration["GmailPassword"];
            });

            services.AddTransient<IWorkHistoryRepo, WorkHistoryRepo>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IGoogleRecaptchaService, GoogleRecaptchaService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            //app.UseCookiePolicy();

            app.UseMvc();
        }
    }
}
