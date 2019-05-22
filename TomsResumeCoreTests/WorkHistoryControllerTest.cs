using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TomsResumeCore;
using TomsResumeCore.Data;
using TomsResumeCore.Models;
using Xunit;

namespace TomsResumeCore.Tests
{
    public class WorkHistoryControllerTest
    {
        WorkHistoryController _controller;
        IWorkHistory _repo;

        public WorkHistoryControllerTest()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            _repo = new WorkHistory(configuration);
            _controller = new WorkHistoryController(_repo);
        }

        [Fact]
        public async Task GetReturnsItemsAsync()
        {
            var result = await _controller.GetAsync();

            Assert.IsAssignableFrom<IEnumerable<JobHistory>>(result);
        }
    }
}
