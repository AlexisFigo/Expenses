using Expenses.Web.Data;
using Expenses.Web.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expenses.Web.Controllers
{
    [Authorize(Roles = "Admin")]
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
                .Include(t => t.City)
                .Include(t => t.User)
                .ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            List<TripDetailsEntity> details = await _context.TripDetails
                .Include(td => td.ExpensesType)
                .Where(td => td.Trip.Id == id)
                .ToListAsync();
            if (details == null)
            {
                return NotFound();
            }
            return View(details);
        }
    }
}