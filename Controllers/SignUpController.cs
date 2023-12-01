using Microsoft.AspNetCore.Mvc;
using OnlineJobPortal.Models;
using System.Data.SqlClient;
using Dapper;

namespace OnlineJobPortal.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SignUpController : ControllerBase
    {
        private readonly IConfiguration configuration;
        public SignUpController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        [HttpPost("signup")]
        public IActionResult SignUp(UserModel newUser)
        {
            // Check if the username is already taken
            if (IsUsernameTaken(newUser.username))
            {
                return BadRequest("Username is already taken!");
            }

            // Hash the password (you should never store plaintext passwords)
            newUser.password = newUser.password;

            // Insert the user into the database
            if (InsertUser(newUser))
            {
                return Ok("User created successfully");
            }
            else
            {
                return BadRequest("Error in inserting data in the database!");
            }
        }
        private List<UserModel> GetUsersList()
        {
            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection").ToString()))
            {
                connection.Open();

                string query = "SELECT * FROM Users";

                List<UserModel> users = connection.Query<UserModel>(query).ToList();

                return users;
            }
        }
        private bool IsUsernameTaken(string username)
        {
            List<UserModel> existingUsers = GetUsersList();
            return existingUsers.Any(u => u.username == username);
        }
        private bool InsertUser(UserModel newUser)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection").ToString()))
                {
                    connection.Open();

                    string insertQuery = "INSERT INTO Users (first_name, middle_name, last_name, username, email, password, user_type)" +
                        " VALUES (@first_name, @middle_name, @last_name, @username, @email, @password, @user_type)";

                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@first_name", newUser.first_name);
                        command.Parameters.AddWithValue("@middle_name", newUser.middle_name);
                        command.Parameters.AddWithValue("@last_name", newUser.last_name);
                        command.Parameters.AddWithValue("@username", newUser.username);
                        command.Parameters.AddWithValue("@email", newUser.email);
                        command.Parameters.AddWithValue("@password", newUser.password); // Hashed password
                        command.Parameters.AddWithValue("@user_type", newUser.user_type);

                        int rowsAffected = command.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
