using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expenses.Web.Data;
using Expenses.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Expenses.Web.Controllers
{
    public class TripsController : Controller
    {
        private readonly DataContext _context;
        public TripsController(DataContext context)
        {
            _context = context;
        }
        public  IActionResult Index()
        {
            IEnumerable<TripsEntity> listTrip = _context.Trips;
                //.Include(t => t.TripDetails)
                //.Include(t => t.Citie)
                //.Include(t => t.User);
            if (listTrip == null)
            {
                return NotFound();
            }
            return View(listTrip);
        }
    }
}