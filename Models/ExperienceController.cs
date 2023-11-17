using Microsoft.AspNetCore.Mvc;
using OnlineJobPortal.Models;
namespace OnlineJobPortal.Controllers
{
    [ApiController]
    [Route("[experiencecontroller]")]
    public class ExperienceController : ControllerBase
    {
        private static readonly IEnumerable<ExperienceModel> Experience = new[]
        {
            new ExperienceModel
            {
                user_id = 1, experience_id = 1, job_title = "Job 1", company_name = "ABC Company",
                start_date = "1-Dec-22", end_date = "1-Jan-23"
            }
        };
        [HttpGet("{experience_id:int}")]
        public ExperienceModel[] Get(int experience_id)
        {
            ExperienceModel[] experience = Experience.Where(i => i.experience_id == experience_id).ToArray();
            return experience;
        }
            }
}
