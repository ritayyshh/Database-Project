using Microsoft.AspNetCore.Mvc;
using OnlineJobPortal.Models;
namespace OnlineJobPortal.Controllers
{
    [ApiController]
    [Route("[educationcontroller]")]
    public class EducationController : ControllerBase
    {
        private static readonly IEnumerable<EducationModel> Education = new[]
        {
            new EducationModel
            {
                user_id = 1, education_id = 1, degree = "Bachelors", major = "Computer Science",
                school = "FAST NUCES"
            }
        };
        [HttpGet("{education_id:int}")]
        public EducationModel[] Get(int education_id)
        {
            EducationModel[] education = Education.Where(i => i.education_id == education_id).ToArray();
            return education;
        }
            }
}
