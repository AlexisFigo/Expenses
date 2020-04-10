using Expenses.Web.Data.Entities;
using Soccer.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expenses.Web.Helper
{
    public class ConverterHelper : IConverterHelper
    {
        public UserResponse ToUserResponse(UserEntity userEntity, string token, DateTime expiration)
        {
            return new UserResponse()
            {
                Id = userEntity.Id,
                Email = userEntity.Email,
                FirstName = userEntity.FirstName,
                LastName = userEntity.LastName,
                UserType = userEntity.UserType,
                Token = token,
                Expiration = expiration
            };
        }
    }
}
