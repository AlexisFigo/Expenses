using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Expenses.Web.Data;
using Expenses.Web.Data.Entities;

namespace Expenses.Web.Controllers
{
    public class ExpensesController : Controller
    {
        private readonly DataContext _context;

        public ExpensesController(DataContext context)
        {
            _context = context;
        }

        // GET: Expenses
        public async Task<IActionResult> Index()
        {
            return View(await _context.ExpensesTypes.ToListAsync());
        }

        // GET: Expenses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Expenses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ExpensesTypeEntity expensesTypeEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(expensesTypeEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(expensesTypeEntity);
        }

       

        // GET: Expenses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expensesTypeEntity = await _context.ExpensesTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expensesTypeEntity == null)
            {
                return NotFound();
            }

            _context.ExpensesTypes.Remove(expensesTypeEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExpensesTypeEntityExists(int id)
        {
            return _context.ExpensesTypes.Any(e => e.Id == id);
        }
    }
}
