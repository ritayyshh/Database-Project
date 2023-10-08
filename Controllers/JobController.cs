using Microsoft.AspNetCore.Mvc;
using OnlineJobPortal.Models;

namespace OnlineJobPortal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JobController : ControllerBase
    {
        private static readonly IEnumerable<JobsModel> Jobs = new[]
        {
            new JobsModel{job_id=12345, company_id="9876543210", job_title="Software Engineer",
                job_description="This is a dummy job description for a Software Engineer position.",
                job_type="Full-time", job_location="San Francisco, CA",
                job_salary=100000, job_post_date="2023-10-08",
                job_deadline="2023-10-22"
            }
        };
        [HttpGet("{job_id:int}")]
        public JobsModel[] Get(int job_id)
        {
            JobsModel[] jobs = Jobs.Where(i => i.job_id == job_id).ToArray();
            return jobs;
        }
    }
}