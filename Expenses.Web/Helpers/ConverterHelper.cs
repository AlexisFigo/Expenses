using Expenses.Common.Models;
using Expenses.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Expenses.Web.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        public List<TripResponse> ToTripResponse(List<TripsEntity> tripsEntity)
        {
            List<TripResponse> trips = new List<TripResponse>();
            foreach (TripsEntity item in tripsEntity)
            {
                trips.Add(
                    new TripResponse
                    {
                        Id = item.Id,
                        StartDate = item.StartDate,
                        EndDate = item.EndDate,
                        Description = item.Description,
                        City = new CityResponse
                        {
                            Id = item.City.Id,
                            Name = item.City.Name
                        },
                        TripDetails = item.TripDetails.Select(td => new TripDetailsResponse
                        {
                            Id = td.Id,
                            ExpensesType = new ExpensesTypeResponse
                            {
                                Id = td.ExpensesType.Id,
                                Name = td.ExpensesType.Name
                            },
                            Date = td.Date,
                            Cost = td.Cost,
                            VoucherPath = td.VoucherPath,
                        }).ToList(),
                    }
                    );
            }
            return trips;
        }

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
