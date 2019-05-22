using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TomsResumeCore.Data;
using TomsResumeCore.Models;

namespace TomsResumeCore
{
    [Route("api/[controller]")]
    public class WorkHistoryController : Controller
    {
        private readonly IWorkHistory _workHistoryData;
        public WorkHistoryController(IWorkHistory workHistoryData)
        {
            _workHistoryData = workHistoryData;
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<IEnumerable<JobHistory>> GetAsync()
        {
            var jobs = await _workHistoryData.GetWorkHistoryAsync();

            return jobs
                .OrderBy(x => x.JobOrder)
                .Select(x => new JobHistory
                {
                    Employer = x.Employer,
                    DateRange = x.DateRange,
                    Position = x.Position,
                    Location = x.Location,
                    JobOrder = x.JobOrder,
                    LogoUrl = x.LogoUrl,
                    BulletPoints = x.BulletPoints
                       .OrderBy(y => y.Order).ToList()
                });
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            throw new NotImplementedException();
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
            throw new NotImplementedException();
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
            throw new NotImplementedException();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
