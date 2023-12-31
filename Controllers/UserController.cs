﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineJobPortal.Models;
using System.Data;
using System.Data.SqlClient;

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
                    user._username = Convert.ToString(dt.Rows[i]["username"]);
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
    }
}