using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineJobPortal.Models;
using System.Data;
using System.Data.SqlClient;

namespace OnlineJobPortal.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        public readonly IConfiguration configuration;
        public CompanyController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        [HttpGet]
        public string GetCompanies()
        {
            SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection").ToString());
            SqlDataAdapter da = new SqlDataAdapter("Select * from Company", connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List <CompanyModel> Companies = new List<CompanyModel> ();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                        CompanyModel company = new CompanyModel();
                        company.company_id = Convert.ToInt32(dt.Rows[i]["company_id"]);
                        company.company_name = Convert.ToString(dt.Rows[i]["company_name"]);
                        company.company_address = Convert.ToString(dt.Rows[i]["company_address"]);
                        company.company_website = Convert.ToString(dt.Rows[i]["company_website"]);
                        company.company_description = Convert.ToString(dt.Rows[i]["company_description"]);
                        Companies.Add(company);
                }
            }
            if (Companies.Count > 0)
                return JsonConvert.SerializeObject(Companies);
            return JsonConvert.SerializeObject(null);
        }
    }
}