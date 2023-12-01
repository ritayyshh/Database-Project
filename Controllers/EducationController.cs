using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineJobPortal.Models;
using System.Data;
using System.Data.SqlClient;

namespace OnlineJobPortal.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EducationController : ControllerBase
    {
        public readonly IConfiguration configuration;
        public EducationController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        [HttpGet]
        public string GetEducation()
        {
            SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection").ToString());
            SqlDataAdapter da = new SqlDataAdapter("Select * from Education", connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List <EducationModel> Education = new List<EducationModel>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    EducationModel education = new EducationModel();
                    education.user_id = Convert.ToInt32(dt.Rows[i]["user_id"]);
                    education.education_id = Convert.ToInt32(dt.Rows[i]["education_id"]);
                    education.degree = Convert.ToString(dt.Rows[i]["degree"]);
                    education.major = Convert.ToString(dt.Rows[i]["major"]);
                    education.school = Convert.ToString(dt.Rows[i]["school"]);
                    Education.Add(education);
                }
            }
            if (Education.Count > 0)
                return JsonConvert.SerializeObject(Education);
            return JsonConvert.SerializeObject(null);
        }
    }
}