using Expenses.Web.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expenses.Web.Data
{
    public class DataContext:IdentityDbContext<UserEntity>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<CitiesEntity> Cities { get; set; }
        public DbSet<CountriesEntity> Countries { get; set; }
        public DbSet<ExpensesTypeEntity> ExpensesTypes { get; set; }
        public DbSet<UserEntity> users { get; set; }
        public DbSet<TripDetailsEntity> TripDetails { get; set; }
        public DbSet<TripsEntity> Trips { get; set; }
        
        
        

    }
}
