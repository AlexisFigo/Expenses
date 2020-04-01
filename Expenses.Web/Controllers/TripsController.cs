using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expenses.Web.Data;
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
            return View();
        }
    }
}