using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class User
    {
        public static User Create(string userName, string firstName, string lastName, string email) 
        {
            return new User(userName, firstName, lastName, email);
        }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        private User(string userName, string firstName, string lastName, string email) : this()
        {
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public User()
        {
        }
    }
}
