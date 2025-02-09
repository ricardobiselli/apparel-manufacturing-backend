using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain.Models.Users
{
    public abstract class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public User(string userName, string name, string lastName, string email, string password)
        {
            UserName = userName;
            Name = name;
            LastName = lastName;
            Email = email;
            Password = password;
        }

    }
}
