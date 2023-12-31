﻿using Microsoft.AspNetCore.Identity;

namespace OnlineJobPortal.Models
{
    public class UserModel : IdentityUser
    {
        public string ?first_name {  get; set; }
        public string ?middle_name { get; set; }
        public string ?last_name { get; set; }
        public int user_id { get; set; }
        public string ?_username { get; set; }
        public string ?email { get; set; }
        public string ?password { get; set; }
        public string ?user_type {  get; set; }
    }
}
