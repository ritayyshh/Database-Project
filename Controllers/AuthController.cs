using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Dapper;
using Microsoft.Extensions.Configuration;
using OnlineJobPortal.Models;
using BCrypt.Net;
using Microsoft.IdentityModel.Tokens;

namespace OnlineJobPortal.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration configuration;

        public AuthController(IConfiguration configuration)
        {
            this.configuration = configuration;
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

        private string GenerateToken(UserModel user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                null,
                expires: DateTime.Now.AddMinutes(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpPost("signup")]
        public IActionResult SignUp(UserModel newUser)
        {
            // Check if the username is already taken
            if (IsUsernameTaken(newUser.username))
            {
                return BadRequest("Username is already taken!");
            }

            // Hash the password (using BCrypt for simplicity)
            newUser.password = BCrypt.Net.BCrypt.HashPassword(newUser.password);

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

        [HttpPost("login")]
        public IActionResult Login(UserModel user)
        {
            var authenticatedUser = AuthenticateUser(user);

            if (authenticatedUser != null)
            {
                // Verify the entered password against the hashed password from the database
                if (BCrypt.Net.BCrypt.Verify(user.password, authenticatedUser.password))
                {
                    var token = GenerateToken(authenticatedUser);
                    return Ok(new { Token = token });
                }
                else
                {
                    return Unauthorized("Invalid username or password");
                }
            }
            else
            {
                return Unauthorized("Invalid username or password");
            }
        }


        private bool IsUsernameTaken(string username)
        {
            List<UserModel> existingUsers = GetUsersList();
            return existingUsers.Exists(u => u.username == username);
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
                // Handle exception
                return false;
            }
        }

        private UserModel AuthenticateUser(UserModel user)
        {
            List<UserModel> _users = GetUsersList();
            return _users.Find(u => u.username == user.username);
        }
    }
}
