using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplicationCapstone.Models
{
    public class AccountModel
    {
        //private int member_role_index = 0;
        private static string[] member_role_usernames = new string[] { "guest", "admin", "subject" };
        private static string[] member_role_passwords = new string[] { "guest", "admin", "subject" };

        public string role_admin_username = member_role_usernames[1];
        public string role_admin_password = member_role_passwords[1];

        public int user_id { get; set; } = 0;
        [Required(ErrorMessage ="Required Field")]
        public string username { get; set; } = member_role_usernames[0];
        [Required(ErrorMessage = "Required Field")]
        [DataType(DataType.Password)]
        public string password { get; set; } = member_role_passwords[0];

        public string login_error_message { get; set; }

    }
}