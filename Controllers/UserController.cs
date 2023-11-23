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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OnlineJobPortal.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly UserManager<UserModel> userManager;
        public UserController(IConfiguration configuration, UserManager<UserModel> userManager)
        {
            this.configuration = configuration;
            this.userManager = userManager;
        }
        private string GenerateToken(UserModel user)
        {
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"], null, 
                expires: DateTime.Now.AddMinutes(1),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        [HttpGet]
        public string GetUsers()
        {
            SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection").ToString());
            SqlDataAdapter da = new SqlDataAdapter("Select * from Users", connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List <UserModel> Users = new List<UserModel>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    UserModel user = new UserModel();
                    user.user_id = Convert.ToInt32(dt.Rows[i]["user_id"]);
                    user.first_name = Convert.ToString(dt.Rows[i]["first_name"]);
                    user.middle_name = Convert.ToString(dt.Rows[i]["middle_name"]);
                    user.last_name = Convert.ToString(dt.Rows[i]["last_name"]);
                    user.username = Convert.ToString(dt.Rows[i]["username"]);
                    user.email = Convert.ToString(dt.Rows[i]["email"]);
                    user.password = Convert.ToString(dt.Rows[i]["password"]);
                    user.user_type = Convert.ToString(dt.Rows[i]["user_type"]);
                    Users.Add(user);

                }
            }
            if (Users.Count > 0)
                return JsonConvert.SerializeObject(Users);
            return JsonConvert.SerializeObject(null);
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] UserModel login)
        {
            var authenticatedUser = AuthenticateUser(login.username, login.password);

            if (authenticatedUser != null)
            {
                var token = GenerateToken(authenticatedUser);
                return Ok(new { Token = token });
            }
            else
            {
                return Unauthorized();
            }
        }

        private UserModel AuthenticateUser(string username, string password)
        {
            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection").ToString()))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand($"SELECT * FROM Users WHERE username = '{username}' AND password = '{password}'", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            UserModel user = new UserModel
                            {
                                user_id = Convert.ToInt32(reader["user_id"]),
                                first_name = Convert.ToString(reader["first_name"]),
                                middle_name = Convert.ToString(reader["middle_name"]),
                                last_name = Convert.ToString(reader["last_name"]),
                                username = Convert.ToString(reader["username"]),
                                email = Convert.ToString(reader["email"]),
                                password = Convert.ToString(reader["password"]),
                                user_type = Convert.ToString(reader["user_type"])
                            };

                            return user;
                        }
                    }
                }
            }

            return null;
        }
        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] UserModel newUser)
        {
            if (await IsUsernameTaken(newUser.username))
            {
                return BadRequest("Username is already taken!");
            }

            var user = new UserModel
            {
                user_id = newUser.user_id,
                first_name = newUser.first_name,
                middle_name = newUser.middle_name,
                last_name = newUser.last_name,
                username = newUser.username,
                email = newUser.email,
                password = newUser.password,
                user_type = newUser.user_type
            };

            var result = await userManager.CreateAsync(user, newUser.password);

            if (result.Succeeded)
            {
                SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection").ToString());
                string insertQuery = "INSERT INTO Books (user_id, first_name, middle_name, last_name, username, email, password, user_type)" +
                    " VALUES (@user_id, @first_name, @middle_name, @last_name, @username, @email, @password, @user_type)";

                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    connection.Open();

                    command.Parameters.AddWithValue("@user_id", newUser.user_id);
                    command.Parameters.AddWithValue("@first_name", newUser.first_name);
                    command.Parameters.AddWithValue("@middle_name", newUser.middle_name);
                    command.Parameters.AddWithValue("@last_name", newUser.last_name);
                    command.Parameters.AddWithValue("@username", newUser.username);
                    command.Parameters.AddWithValue("@email", newUser.email);
                    command.Parameters.AddWithValue("@password", newUser.password);
                    command.Parameters.AddWithValue("@user_type", newUser.user_type);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        connection.Close();
                        return Ok("User created successfully");
                    }
                    else
                    {
                        await userManager.DeleteAsync(user);
                        return BadRequest("Error in inserting data in database!");
                    }
                }
            }
            else
                return BadRequest(result.Errors);
        }

        private async Task<bool> IsUsernameTaken(string username)
        {
            var existingUser = await userManager.FindByNameAsync(username);
            return existingUser != null;
        }
    }
}