using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoonSpace.Data;
using MoonSpace.Models;

namespace MoonSpace.Controllers
{
    public class MoonSpaceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MoonSpaceController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Start()
        {
            //Check if someone is logged in or not
            // And if logged in and nog i registerd cusomer, redirect to view that lets you rigester your costumer data
            var costumer = _context.Costumer
                .Where(obj => obj.Email == User.Identity.Name)
                .FirstOrDefault(); 
            
            if (User.Identity.Name == "emmeli@moonspace.com")
            {
                return View();
            }
            else if (!User.Identity.IsAuthenticated)
            {
                return View();
            }
            else if (costumer == null)
            {
                return RedirectToAction("CreateCostumer", "MoonSpace");
            }
            else
            {
                return View();
            }
        }

        public IActionResult About()
        {
            return View();
        }


        // GET: Costumer/Create
        [Authorize]
        public IActionResult CreateCostumer()
        {
            return View();
        }

        // POST: Costumer/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCostumer([Bind("CostumerId,FirstName,LastName,Email,PhoneNumber")] Costumer costumer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(costumer);
                await _context.SaveChangesAsync();
                return RedirectToAction("Start", "MoonSpace");
            }
            return View(costumer);
        }

        // GET: MoonSpaceController
        [Authorize]
        public async Task<IActionResult> Products(string searchString)
        {
            var TypeList = _context.Category.FromSqlRaw("Select * from Category");
            ViewBag.TypeList = TypeList;

            var products = from m in _context.Product
                           select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(s => s.ProductName.ToLower().Contains(searchString.ToLower())
                                            || s.Category.ToLower().Contains(searchString.ToLower())
                                            || s.Description.ToLower().Contains(searchString.ToLower()));
            }

            return View(await products.ToListAsync());
        }

        // GET: Product/Details/5
        [Authorize]
        public async Task<IActionResult> ProductDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }


        // GET: Orders/Create
        [Authorize]
        public IActionResult CreateOrder([FromQuery] int ProductId)
        {
            if (ProductId == 0)
            {
                return NotFound();
            }
            // Get product Id
            HttpContext.Session.SetInt32("ProductId", ProductId);
            ViewBag.productId = ProductId;

            //Get costumer id for loged in user
            var costumer = _context.Costumer
            .Where(obj => obj.Email == User.Identity.Name);
            foreach (var item in costumer)
            {
                ViewBag.costumer = item.CostumerId;
            }

            //Get product  for loged in user
            var product = _context.Product
            .Where(obj => obj.ProductId == ProductId);
            foreach (var item in product)
            {
                ViewBag.ProductName = item.ProductName;
                ViewBag.ProductPrice = item.ProductPrice;

            }

            if (costumer == null)
            {
                return NotFound();
            }
            return View();
        }


        // POST: Orders/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrder([Bind("OrderID,ProductId,OrderDate,Paid,CostumerId")] Orders orders)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orders);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(OrderConfirmed));
            }
            return View(orders);
        }

        [Authorize]
        public IActionResult OrderConfirmed()
        {
            return View();
        }
    }
}
