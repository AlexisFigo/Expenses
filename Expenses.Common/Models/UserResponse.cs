using Expenses.Common.Enums;
using System;

namespace Expenses.Common.Models
{
    public class UserResponse
    {
        public string Id { get; set; }

        public string Document { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public UserType UserType { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public string Token { get; set; }

        public DateTime Expiration { get; set; }

    }
}
