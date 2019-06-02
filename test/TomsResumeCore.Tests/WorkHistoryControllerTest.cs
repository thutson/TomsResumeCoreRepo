using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TomsResumeCore.DomainModels;
using TomsResumeCore.Data;
using TomsResumeCore.Web.API;
using Xunit;
using Microsoft.AspNetCore.Mvc;

namespace TomsResumeCore.Tests
{
    public class WorkHistoryControllerTest
    {
        private readonly WorkHistoryController _controller;
        private readonly IWorkHistoryRepo _repo;

        public WorkHistoryControllerTest()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            _repo = new WorkHistoryRepo(configuration);
            _controller = new WorkHistoryController(_repo);
        }

        [Fact]
        public async Task GetReturnsItemsAsync()
        {
            var result = await _controller.Get();

            Assert.IsAssignableFrom<ActionResult<IEnumerable<JobHeld>>>(result);
        }
    }
}
