using Expenses.Web.Data.Entitis;
using Expenses.Web.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expenses.Web.Helper
{
    public class UserHelper : IUserHelper
    {
        public Task<IdentityResult> AddUserAsync(UserEntity user, string password)
        {
            return null;
        }

        public Task AddUserToRoleAsync(UserEntity user, string roleName)
        {
            return null;
        }

        public Task CheckRoleAsync(string roleName)
        {
            return null;
        }

        public Task<UserEntity> GetUserByEmailAsync(string email)
        {
            return null;
        }

        public Task<UserEntity> GetUserByName(string email)
        {
            return null;
        }

        public Task<bool> IsUserInRoleAsync(UserEntity user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task<SignInResult> LoginAsync(LoginViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task LogoutAsync()
        {
            throw new NotImplementedException();
        }
    }
}
