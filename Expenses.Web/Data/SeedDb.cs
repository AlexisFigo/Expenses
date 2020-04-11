using Expenses.Web.Data.Entities;
using Expenses.Web.Helpers;
using Expenses.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expenses.Web.Data
{
    public class SeedDb
    {

        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckRolesAsync();
            await CheckExpensesTypeAsync();
            await CheckCountriesAsync();
            await CheckUserAsync("Alexis", "Correa", "figo1813@gmail.com", UserType.Admin);
            await CheckUserAsync("Alexis", "Correa", "alexisfigo18@hotmail.com", UserType.User);
            await CheckUserAsync("Alexis", "Correa", "alexis.correa.cano@outlook.com", UserType.User);
            await CheckUserAsync("Alexis", "Correa", "alexiscorrea231191@correo.itm.edu.co", UserType.User);
            await CheckTripsAsync();
        }

        private async Task CheckRolesAsync()
        {
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.User.ToString()); 
        }

        private async Task CheckExpensesTypeAsync()
        {
            if (!_context.ExpensesTypes.Any())
            {
                AddExpensesType("Taxi");
                AddExpensesType("Food");
                AddExpensesType("Photocopies");
                AddExpensesType("Flight tickets");
                AddExpensesType("Others");
                await _context.SaveChangesAsync();
            }
        }

        private void AddExpensesType(string name)
        {
            _context.ExpensesTypes.Add(new ExpensesTypeEntity
            {
                Name = name
            });
        }

        private async Task CheckCountriesAsync()
        {
            if (!_context.Countries.Any())
            {
                AddCountrie("Colomia", new List<CitiesEntity>()
                {
                    new CitiesEntity
                    {
                        Name = "Bogota"
                    },
                    new CitiesEntity
                    {
                        Name = "Medellin"
                    },
                    new CitiesEntity
                    {
                        Name = "Cali"
                    }
                });

                AddCountrie("España", new List<CitiesEntity>()
                {
                    new CitiesEntity
                    {
                        Name = "Barcelona"
                    },
                    new CitiesEntity
                    {
                        Name = "Madrid"
                    },
                    new CitiesEntity
                    {
                        Name = "Valencia"
                    }
                });
                AddCountrie("Argentina", new List<CitiesEntity>()
                {
                    new CitiesEntity
                    {
                        Name = "Bunos aires"
                    },
                    new CitiesEntity
                    {
                        Name = "Mendoza"
                    },
                    new CitiesEntity
                    {
                        Name = "Rosario"
                    }
                });
                await _context.SaveChangesAsync();
            }
        }

        private void AddCountrie(string name, ICollection<CitiesEntity> cities)
        {
            _context.Countries.Add(new CountriesEntity
            {
                Name = name,
                Cities = cities
            });
        }

        private async Task CheckUserAsync(string name, string lastName, string email, UserType userType)
        {
            UserEntity user = await _userHelper.GetUserByEmailAsync(email);
            if (user == null)
            {
                user = new UserEntity
                {
                    FirstName = name,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    UserType = userType
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());

                var token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                await _userHelper.ConfirmEmailAsync(user, token);
            }

        }

        private async Task CheckTripsAsync()
        {
            if (!_context.Trips.Any())
            {
                foreach (UserEntity user in _context.Users)
                {
                    if (user.UserType == UserType.User)
                    {
                        AddTrip(user);
                    }
                }

                await _context.SaveChangesAsync();
            }
        }

        private async void AddTrip(UserEntity user)
        {
            DateTime startDate = DateTime.Today.AddMonths(2).ToUniversalTime();
            DateTime endDate = DateTime.Today.AddMonths(3).ToUniversalTime();

            _context.Trips.Add(new TripsEntity
            {
                StartDate = startDate,
                EndDate = endDate,
                User = user,
                Description = "Viaje de negocios",
                City = _context.Cities.FirstOrDefault(c => c.Name == "Bogota"),
                TripDetails = new List<TripDetailsEntity>
                 {
                    new TripDetailsEntity
                    {
                        Date = startDate.AddDays(1),
                        ExpensesType = _context.ExpensesTypes.FirstOrDefault(e => e.Name == "Food"),
                        Cost = 125
                    },
                     new TripDetailsEntity
                    {
                        Date = startDate.AddDays(1),
                        ExpensesType = _context.ExpensesTypes.FirstOrDefault(e => e.Name == "Taxi"),
                        Cost = 12
                    },
                },
            });
            _context.Trips.Add(new TripsEntity
            {
                StartDate = startDate,
                EndDate = endDate,
                User = user,
                Description = "Viaje de negocios",
                City = _context.Cities.FirstOrDefault(c => c.Name == "Medellin"),
                TripDetails = new List<TripDetailsEntity>
                 {
                    new TripDetailsEntity
                    {
                        Date = startDate.AddDays(1),
                        ExpensesType = _context.ExpensesTypes.FirstOrDefault(e => e.Name == "Food"),
                        Cost = 125
                    },
                     new TripDetailsEntity
                    {
                        Date = startDate.AddDays(1),
                        ExpensesType = _context.ExpensesTypes.FirstOrDefault(e => e.Name == "Taxi"),
                        Cost = 12
                    },
                },
            });
            _context.Trips.Add(new TripsEntity
            {
                StartDate = startDate,
                EndDate = endDate,
                User = user,
                Description = "Viaje de charla con clientes",
                City = _context.Cities.FirstOrDefault(c => c.Name == "Barcelona"),
                TripDetails = new List<TripDetailsEntity>
                {
                    new TripDetailsEntity
                    {
                         Date = startDate.AddDays(1),
                         ExpensesType = _context.ExpensesTypes.FirstOrDefault(e => e.Name == "Food"),
                         Cost = 125
                    },
                    new TripDetailsEntity
                    {
                        Date = startDate.AddDays(1),
                        ExpensesType = _context.ExpensesTypes.FirstOrDefault(e => e.Name == "Taxi"),
                        Cost = 12
                    },
                },
            });
            _context.Trips.Add(new TripsEntity
            {
                StartDate = startDate,
                EndDate = endDate,
                User = user,
                Description = "Viaje de charla con clientes",
                City = _context.Cities.FirstOrDefault(c => c.Name == "Madrid"),
                TripDetails = new List<TripDetailsEntity>
                {
                    new TripDetailsEntity
                    {
                         Date = startDate.AddDays(1),
                         ExpensesType = _context.ExpensesTypes.FirstOrDefault(e => e.Name == "Food"),
                         Cost = 125
                    },
                    new TripDetailsEntity
                    {
                        Date = startDate.AddDays(1),
                        ExpensesType = _context.ExpensesTypes.FirstOrDefault(e => e.Name == "Taxi"),
                        Cost = 12
                    },
                },
            });
            _context.Trips.Add(new TripsEntity
            {
                StartDate = startDate,
                EndDate = endDate,
                User = user,
                Description = "Viaje de charla con clientes",
                City = _context.Cities.FirstOrDefault(c => c.Name == "Mendoza"),
                TripDetails = new List<TripDetailsEntity>
                {
                    new TripDetailsEntity
                    {
                         Date = startDate.AddDays(1),
                         ExpensesType = _context.ExpensesTypes.FirstOrDefault(e => e.Name == "Food"),
                         Cost = 125
                    },
                    new TripDetailsEntity
                    {
                        Date = startDate.AddDays(1),
                        ExpensesType = _context.ExpensesTypes.FirstOrDefault(e => e.Name == "Taxi"),
                        Cost = 12
                    },
                },
            });
            _context.Trips.Add(new TripsEntity
            {
                StartDate = startDate,
                EndDate = endDate,
                User = user,
                Description = "Viaje de curso",
                City = _context.Cities.FirstOrDefault(c => c.Name == "Rosario"),
                TripDetails = new List<TripDetailsEntity>
                {
                    new TripDetailsEntity
                    {
                         Date = startDate.AddDays(1),
                         ExpensesType = _context.ExpensesTypes.FirstOrDefault(e => e.Name == "Food"),
                         Cost = 125
                    },
                    new TripDetailsEntity
                    {
                        Date = startDate.AddDays(1),
                        ExpensesType = _context.ExpensesTypes.FirstOrDefault(e => e.Name == "Taxi"),
                        Cost = 12
                    },
                },
            });
        }
    }
}
