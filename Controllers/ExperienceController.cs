using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OnlineJobPortal.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Transactions;

namespace OnlineJobPortal.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ExperienceController : ControllerBase
    {
        public readonly IConfiguration configuration;
        public ExperienceController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        [HttpGet]
        public string GetExperiences()
        {
            SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection").ToString());
            SqlDataAdapter da = new SqlDataAdapter("Select * from Experience", connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List <ExperienceModel> Experiences = new List<ExperienceModel>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ExperienceModel experience = new ExperienceModel();
                    experience.user_id = Convert.ToInt32(dt.Rows[i]["user_id"]);
                    experience.experience_id = Convert.ToInt32(dt.Rows[i]["experience_id"]);
                    experience.job_title = Convert.ToString(dt.Rows[i]["job_title"]);
                    experience.company_name = Convert.ToString(dt.Rows[i]["company_name"]);
                    experience.start_date = Convert.ToString(dt.Rows[i]["start_date"]);
                    experience.end_date = Convert.ToString(dt.Rows[i]["end_date"]);
                    Experiences.Add(experience);

                }
            }
            if (Experiences.Count > 0)
                return JsonConvert.SerializeObject(Experiences);
            return JsonConvert.SerializeObject(null);
        }
    }
}