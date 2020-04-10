using Expenses.Web.Data.Entities;
using Expenses.Common.Models;
using System;
using System.Collections.Generic;

namespace Expenses.Web.Helpers
{
    public interface IConverterHelper
    {
        UserResponse ToUserResponse(UserEntity userEntity, string token, DateTime expiration);

        List<TripResponse> ToTripResponse(List<TripsEntity> tripsEntity);
    }
}
