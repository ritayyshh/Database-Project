using Microsoft.AspNetCore.Mvc;
using OnlineJobPortal.Models;
namespace OnlineJobPortal.Controllers
{
    [ApiController]
    [Route("[usercontroller]")]
    public class UserController : ControllerBase
    {
        private static readonly IEnumerable<UserModel> Users = new[]
        {
            new UserModel
            {
                first_name = "First Name", middle_name = "Middle Name", last_name = "Last Name",
                user_id = 1, username = "example123", email = "example123@gmail.com", password = "password",
                user_type = "Job Seeker"
            }
        };
        [HttpGet("{user_id:int}")]
        public UserModel[] Get(int user_id)
        {
            UserModel[] users = Users.Where(i => i.user_id == user_id).ToArray();
            return users;
        }
    }
}
