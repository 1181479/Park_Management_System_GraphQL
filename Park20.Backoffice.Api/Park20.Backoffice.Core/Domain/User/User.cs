using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park20.Backoffice.Core.Domain.User
{
    public abstract class User
    {
        protected User() { }
        public User(string name, string email, string password, string username)
        {
            Name = name;
            Email = email;
            Password = password;
            Username = username;
        }
        public string Name { get; private set; }
        public string Password { get; private set; }
        public string Email { get; private set; }
        public string Username { get; private set; }
        public DateTime RegistrationDate { get; private set; }
    }
}
