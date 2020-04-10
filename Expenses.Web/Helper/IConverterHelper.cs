using Expenses.Web.Data.Entities;
using Soccer.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expenses.Web.Helper
{
    public interface IConverterHelper
    {
        UserResponse ToUserResponse(UserEntity userEntity, string token, DateTime expiration);
    }
}
