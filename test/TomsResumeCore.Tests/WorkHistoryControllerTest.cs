using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TomsResumeCore.DomainModels;
using TomsResumeCore.Data;
using TomsResumeCore.Web.API;
using Xunit;

namespace TomsResumeCore.Tests
{
    public class WorkHistoryControllerTest
    {
        readonly WorkHistoryController _controller;
        readonly IWorkHistoryRepo _repo;

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
            var result = await _controller.GetAsync();

            Assert.IsAssignableFrom<IEnumerable<JobHeld>>(result);
        }
    }
}
