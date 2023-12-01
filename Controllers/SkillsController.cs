using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineJobPortal.Models;
using System.Data;
using System.Data.SqlClient;

namespace OnlineJobPortal.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        public readonly IConfiguration configuration;
        public SkillsController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        [HttpGet]
        public string GetSkills()
        {
            SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection").ToString());
            SqlDataAdapter da = new SqlDataAdapter("Select * from Skills", connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List < SkillsModel > Skills = new List<SkillsModel> ();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    SkillsModel skill = new SkillsModel();
                    skill.user_id = Convert.ToInt32(dt.Rows[i]["user_id"]);
                    skill.skill_id = Convert.ToInt32(dt.Rows[i]["skill_id"]);
                    skill.skill_name = Convert.ToString(dt.Rows[i]["skill_name"]);
                    Skills.Add(skill);
                }
            }
            if (Skills.Count > 0)
                return JsonConvert.SerializeObject(Skills);
            return JsonConvert.SerializeObject(null);
        }
    }
}