using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Dapper;
using OnlineJobPortal.Models;
using BCrypt.Net;

namespace OnlineJobPortal.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private List<UserModel> GetUsersList()
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                string query = "SELECT * FROM Users";

                List<UserModel> users = connection.Query<UserModel>(query).ToList();

                return users;
            }
        }

        private string GenerateToken(UserModel user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                null,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpirationMinutes"])),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpPost("signup")]
        public IActionResult SignUp(UserModel newUser)
        {
            try
            {
                if (IsUsernameTaken(newUser._username))
                {
                    return BadRequest("Username is already taken!");
                }

                newUser.password = BCrypt.Net.BCrypt.HashPassword(newUser.password);

                if (InsertUser(newUser))
                {
                    return Ok("User created successfully");
                }
                else
                {
                    return BadRequest("Error in inserting data in the database!");
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost("login")]
        public IActionResult Login(UserModel user)
        {
            var authenticatedUser = AuthenticateUser(user);

            if (authenticatedUser != null && BCrypt.Net.BCrypt.Verify(user.password, authenticatedUser.password))
            {
                var token = GenerateToken(authenticatedUser);
                return Ok(new { Token = token });
            }

            return Unauthorized("Invalid username or password");
        }
        private bool IsUsernameTaken(string username_)
        {
            List<UserModel> existingUsers = GetUsersList();
            return existingUsers.Exists(u => u._username == username_);
        }

        private bool InsertUser(UserModel newUser)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();

                    string insertQuery = "INSERT INTO Users (first_name, middle_name, last_name, username, email, password, user_type)" +
                                         " VALUES (@first_name, @middle_name, @last_name, @username, @email, @password, @user_type)";

                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@first_name", newUser.first_name);
                        command.Parameters.AddWithValue("@middle_name", newUser.middle_name);
                        command.Parameters.AddWithValue("@last_name", newUser.last_name);
                        command.Parameters.AddWithValue("@username", newUser._username);
                        command.Parameters.AddWithValue("@email", newUser.email);
                        command.Parameters.AddWithValue("@password", newUser.password);
                        command.Parameters.AddWithValue("@user_type", newUser.user_type);

                        int rowsAffected = command.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                return false;
            }
        }

        private UserModel AuthenticateUser(UserModel user)
        {
            List<UserModel> users = GetUsersList();
            return users.Find(u => u._username == user._username);
        }
    }
}