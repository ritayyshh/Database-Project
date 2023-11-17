using Microsoft.AspNetCore.Mvc;
using OnlineJobPortal.Models;
namespace OnlineJobPortal.Controllers
{
    [ApiController]
    [Route("[companycontroller]")]
    public class CompanyController : ControllerBase
    {
        private static readonly IEnumerable<CompanyModel> Companies = new[]
        {
            new CompanyModel
            {
                company_id = 1, company_name = "ABC Company", company_address = "ABC street",
                company_website = "www.google.com", company_description = "Description"
            }
        };
        [HttpGet("{company_id:int}")]
        public CompanyModel[] Get(int company_id)
        {
            CompanyModel[] companies = Companies.Where(i => i.company_id == company_id).ToArray();
            return companies;
        }
    }
}
