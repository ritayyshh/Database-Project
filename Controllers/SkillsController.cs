using Microsoft.AspNetCore.Mvc;
using OnlineJobPortal.Models;
namespace OnlineJobPortal.Controllers
{
    [ApiController]
    [Route("[skillscontroller]")]
    public class SkillsController : ControllerBase
    {
        private static readonly IEnumerable<SkillsModel> Skills = new[]
        {
            new SkillsModel
            {
                user_id = 1, skill_id = 1, skill_name = "Skill"
            }
        };
        [HttpGet("{skill_id:int}")]
        public SkillsModel[] Get(int skill_id)
        {
            SkillsModel[] skills = Skills.Where(i => i.skill_id == skill_id).ToArray();
            return skills;
        }
            }
}
