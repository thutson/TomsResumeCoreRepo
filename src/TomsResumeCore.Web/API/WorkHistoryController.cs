using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TomsResumeCore.DomainModels;
using TomsResumeCore.Data;

namespace TomsResumeCore.Web.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkHistoryController : ControllerBase
    {
        private readonly IWorkHistoryRepo _workHistoryRepo;
        public WorkHistoryController(IWorkHistoryRepo workHistoryRepo)
        {
            _workHistoryRepo = workHistoryRepo;
        }

        // GET: api/<controller>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<JobHeld>>> Get()
        {
            var jobs = await _workHistoryRepo.GetWorkHistoryAsync();

            return Ok(
                jobs
                    .OrderByDescending(x => x.JobOrder)
                    .Select(x => new JobHeld
                    {
                        Employer = x.Employer,
                        DateRange = x.DateRange,
                        Position = x.Position,
                        Location = x.Location,
                        JobOrder = x.JobOrder,
                        LogoUrl = x.LogoUrl,
                        BulletPoints = x.BulletPoints
                            .OrderBy(y => y.Order).ToList()
                    })
                );
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
