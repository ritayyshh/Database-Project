using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using OnlineJobPortal.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
namespace OnlineJobPortal.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration configuration;
        public LoginController(IConfiguration configuration)
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
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"], null,
                expires: DateTime.Now.AddMinutes(1),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
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
    }
}
