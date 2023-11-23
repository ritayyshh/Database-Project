using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineJobPortal.Models;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace OnlineJobPortal.Controllers
{
    [Route("jobcontroller")]
    [ApiController]
    public class JobController : ControllerBase
    {
        public readonly IConfiguration configuration;
        public JobController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        [HttpGet]
        [Route("GetAllJobs")]
        public string GetJobs()
        {
            SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection").ToString());
            SqlDataAdapter da = new SqlDataAdapter("Select * from JobPost", connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<JobsModel> Jobs = new List<JobsModel>();
            if(dt.Rows.Count > 0)
            {
                for(int i = 0; i < dt.Rows.Count; i++)
                {
                    JobsModel job = new JobsModel();
                    job.job_id = Convert.ToInt32(dt.Rows[i]["job_id"]);
                    job.company_id = Convert.ToInt32(dt.Rows[i]["company_id"]);
                    job.job_title = Convert.ToString(dt.Rows[i]["job_title"]);
                    job.job_description = Convert.ToString(dt.Rows[i]["job_description"]);
                    job.job_location = Convert.ToString(dt.Rows[i]["job_location"]);
                    job.job_type = Convert.ToString(dt.Rows[i]["job_type"]);
                    job.job_salary = Convert.ToInt32(dt.Rows[i]["job_salary"]);
                    job.job_posted_date = Convert.ToString(dt.Rows[i]["job_posted_date"]);
                    job.job_deadline = Convert.ToString(dt.Rows[i]["job_deadline"]);
                    Jobs.Add(job);
                }
            }
            if (Jobs.Count > 0)
                return JsonConvert.SerializeObject(Jobs);
            return JsonConvert.SerializeObject(null);
        }
    }
}