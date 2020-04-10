using Expenses.Web.Data.Entities;
using Expenses.Web.Models;
using Microsoft.AspNetCore.Identity;
using Expenses.Common.Enums;
using Soccer.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expenses.Web.Helpers
{
    public interface IUserHelper
    {
        Task<IdentityResult> ResetPasswordAsync(UserEntity user, string token, string password);
        Task<UserEntity> GetUserAsync(Guid userId);

        Task<string> GenerateEmailConfirmationTokenAsync(UserEntity user);

        Task<IdentityResult> ConfirmEmailAsync(UserEntity user, string token);

        Task<IdentityResult> ChangePasswordAsync(UserEntity user, string oldPassword, string newPassword);

        Task<IdentityResult> UpdateUserAsync(UserEntity user);

        Task<string> GeneratePasswordResetTokenAsync(UserEntity user);

        Task<UserEntity> AddUserAsync(AddUserViewModel model, string path, UserType userType);

        Task<UserEntity> GetUserAsync(string email);

        Task<SignInResult> ValidatePasswordAsync(UserEntity user, string password);
       
        Task<UserEntity> GetUserByEmailAsync(string email);
        
        Task<UserEntity> GetUserByName(string email);
       
        Task<IdentityResult> AddUserAsync(UserEntity user, string password);
        
        Task CheckRoleAsync(string roleName);
       
        Task AddUserToRoleAsync(UserEntity user, string roleName);
       
        Task<bool> IsUserInRoleAsync(UserEntity user, string roleName);
       
        Task<SignInResult> LoginAsync(LoginViewModel model);
       
        Task LogoutAsync();

    }
}
