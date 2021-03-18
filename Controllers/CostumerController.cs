using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoonSpace.Data;
using MoonSpace.Models;

namespace MoonSpace.Controllers
{
    public class CostumerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CostumerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Costumer
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Costumer.ToListAsync());
        }

        // GET: Costumer/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var costumer = await _context.Costumer
                .FirstOrDefaultAsync(m => m.CostumerId == id);
            if (costumer == null)
            {
                return NotFound();
            }

            return View(costumer);
        }

        // GET: Costumer/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Costumer/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CostumerId,FirstName,LastName,Email,PhoneNumber")] Costumer costumer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(costumer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(costumer);
        }

        // GET: Costumer/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var costumer = await _context.Costumer.FindAsync(id);
            if (costumer == null)
            {
                return NotFound();
            }
            return View(costumer);
        }

        // POST: Costumer/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CostumerId,FirstName,LastName,Email,PhoneNumber")] Costumer costumer)
        {
            if (id != costumer.CostumerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(costumer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CostumerExists(costumer.CostumerId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(costumer);
        }

        // GET: Costumer/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var costumer = await _context.Costumer
                .FirstOrDefaultAsync(m => m.CostumerId == id);
            if (costumer == null)
            {
                return NotFound();
            }

            return View(costumer);
        }

        // POST: Costumer/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var costumer = await _context.Costumer.FindAsync(id);
            _context.Costumer.Remove(costumer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CostumerExists(int id)
        {
            return _context.Costumer.Any(e => e.CostumerId == id);
        }
    }
}
