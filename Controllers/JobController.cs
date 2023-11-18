using Microsoft.AspNetCore.Mvc;
using OnlineJobPortal.Models;

namespace OnlineJobPortal.Controllers
{
    [ApiController]
    [Route("job")]
    public class JobController : ControllerBase
    {
        private static readonly IEnumerable<JobsModel> Jobs = new[]
        {
           new JobsModel
            {
                job_id=12345, company_id=1, job_title="Software Engineer",
                job_description="This is a dummy job description for a Software Engineer position.",
                job_type="Full-time", job_location="San Francisco, CA", job_salary=100000,
                job_post_date="2023-10-08", job_deadline="2023-10-22"
            },
            new JobsModel
            {
                job_id=12346, company_id=2, job_title="Web Developer",
                job_description="This is a dummy job description for a Web Developer position.",
                job_type="Contract", job_location="New York, NY", job_salary=90000,
                job_post_date="2023-10-10", job_deadline="2023-10-24"
            },
            new JobsModel
            {
                job_id=12347, company_id=3, job_title="Data Scientist",
                job_description="This is a dummy job description for a Data Scientist position.",
                job_type="Part-time", job_location="Seattle, WA", job_salary=120000,
                job_post_date="2023-10-12", job_deadline="2023-10-26"
            },
            new JobsModel
            {
                job_id=12348, company_id=4, job_title="UX/UI Designer",
                job_description="This is a dummy job description for a UX/UI Designer position.",
                job_type="Full-time", job_location="Austin, TX", job_salary=95000,
                job_post_date="2023-10-15", job_deadline="2023-10-29"
            },
            new JobsModel
            {
                job_id=12349, company_id=5, job_title="Network Administrator",
                job_description="This is a dummy job description for a Network Administrator position.",
                job_type="Contract", job_location="Chicago, IL", job_salary=85000,
                job_post_date="2023-10-18", job_deadline="2023-11-01"
            }
        };
        [HttpGet("{job_id:int}")]
        public JobsModel[] Get(int job_id)
        {
            JobsModel[] jobs = Jobs.Where(i => i.job_id == job_id).ToArray();
            return jobs;
        }

        [HttpGet]
        
        public JobsModel[] GetAll()
        {
            JobsModel[] jobs = Jobs.ToArray();
            return jobs; ;
        }
    }
}