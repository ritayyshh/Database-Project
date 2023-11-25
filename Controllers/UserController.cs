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
using Dapper;

namespace OnlineJobPortal.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration configuration;
        public UserController(IConfiguration configuration)
        {
            this.configuration = configuration;
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
            List<UserModel> Users = new List<UserModel>();
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
        [HttpPost("login")]
        public IActionResult Login(UserModel user)
        {
            var authenticatedUser = AuthenticateUser(user);

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
        private UserModel AuthenticateUser(UserModel user)
        {
            List<UserModel> _users = GetUsersList();
            UserModel AuthenticatedUser = _users.FirstOrDefault(u => u.username == user.username);

            if (AuthenticatedUser != null)
            {
                if (AuthenticatedUser.password == user.password)
                    return AuthenticatedUser;
            }

            return null;
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