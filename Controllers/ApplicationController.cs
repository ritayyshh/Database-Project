using Microsoft.AspNetCore.Mvc;
using OnlineJobPortal.Models;
namespace OnlineJobPortal.Controllers
{
    [ApiController]
    [Route("[applicationcontroller]")]
    public class ApplicationController : ControllerBase
    {
        private static readonly IEnumerable<ApplicationModel> Applications = new[]
        {
            new ApplicationModel
            {
                application_id = 1, job_id = 1234, user_id = 1, application_status = "Pending", 
                application_date = "017-nov-23", suitable_interview_time = "11 am Monday, 20 nov 23"
            }
        };
        [HttpGet("{application_id:int}")]
        public ApplicationModel[] Get(int application_id)
        {
            ApplicationModel[] applications = Applications.Where(i => i.application_id == application_id).ToArray();
            return applications;
        }
            }
}
