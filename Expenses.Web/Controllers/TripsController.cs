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
        public async Task<IActionResult> Index()
        {
            return View(await _context.Trips
                .Include(t => t.TripDetails)
                .Include(t => t.Citie)
                .Include(t => t.User)
                .ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var details = await _context.TripDetails
                .Include(td => td.ExpensesType)
                .Where(td => td.trip.Id == id)
                .ToListAsync();
            if (details == null)
            {
                return NotFound();
            }
            return View(details);
        }
    }
}