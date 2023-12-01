using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineJobPortal.Models;
using System.Data;
using System.Data.SqlClient;

namespace OnlineJobPortal.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        public readonly IConfiguration configuration;
        public ApplicationController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        [HttpGet]
        public string GetApplications()
        {
            SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection").ToString());
            SqlDataAdapter da = new SqlDataAdapter("Select * from Application", connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<ApplicationModel> Applications = new List<ApplicationModel> ();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                        ApplicationModel application = new ApplicationModel();
                        application.application_id = Convert.ToInt32(dt.Rows[i]["application_id"]);
                        application.job_id = Convert.ToInt32(dt.Rows[i]["job_id"]);
                        application.user_id = Convert.ToInt32(dt.Rows[i]["user_id"]);
                        application.application_status = Convert.ToString(dt.Rows[i]["application_status"]);
                        application.application_date = Convert.ToString(dt.Rows[i]["application_date"]);
                        application.suitable_interview_time = Convert.ToString(dt.Rows[i]["suitable_interview_time"]);
                        Applications.Add(application);
                    }
                }
            if (Applications.Count > 0)
                return JsonConvert.SerializeObject(Applications);
            return JsonConvert.SerializeObject(null);
        }
    }
}