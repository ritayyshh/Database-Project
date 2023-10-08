using Microsoft.AspNetCore.Mvc;

namespace OnlineJobPortal.Controllers
{
    [ApiController]
    [Route("[Job]")]
    public class JobController : ControllerBase
    {
        private readonly ILogger<JobController> _logger;
        public JobController(ILogger<JobController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public string Index()
        {
            return "Test view for Job controller";
        }
    }
}
