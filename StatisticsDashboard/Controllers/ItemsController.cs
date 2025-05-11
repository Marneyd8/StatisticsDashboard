using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StatisticsDashboard.Data;
using StatisticsDashboard.Models;

namespace StatisticsDashboard.Controllers
{
    public class ItemsController : Controller
    {
        private readonly MyAppContext _context;
        public ItemsController(MyAppContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var item = await _context.Items.Include(c => c.Category)
                                            .Include(ic => ic.ItemClients)
                                            .ThenInclude(c => c.Client)
                                            .ToListAsync();
            return View(item);
        }

        public async Task<IActionResult> CreateForClient(int clientId)
        {
            var client = await _context.Clients.FindAsync(clientId);
            if (client == null)
            {
                return NotFound();
            }

            ViewBag.ClientId = clientId;
            ViewBag.Categories = _context.Categories.ToList();
            return View("CreateForClient");
        }

        [HttpPost]
        public async Task<IActionResult> CreateForClient(Item item, int clientId)
        {
            if (ModelState.IsValid)
            {
                _context.Items.Add(item);
                await _context.SaveChangesAsync();
                var itemClient = new ItemClient
                {
                    ItemId = item.Id,
                    ClientId = clientId
                };
                _context.Add(itemClient);
                await _context.SaveChangesAsync();

                return RedirectToAction("Profile", "Clients", new { id = clientId });
            }

            ViewBag.ClientId = clientId;
            ViewBag.Categories = _context.Categories.ToList();
            return View(item);
        }

        public IActionResult Create()
        {
            ViewData["Categories"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id, Name, Price, CategoryId")] Item item)
        {
            if (ModelState.IsValid)
            {
                _context.Items.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction("index");
            }
            return View(item);
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewData["Categories"] = new SelectList(_context.Categories, "Id", "Name");
            var item = await _context.Items.FirstOrDefaultAsync(x => x.Id == id);
            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Name, Price, CategoryId")] Item item)
        {
            if (ModelState.IsValid)
            {
                _context.Update(item);
                await _context.SaveChangesAsync();
                return RedirectToAction("index");
            }
            return View(item);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Items.FirstOrDefaultAsync(x => x.Id == id);
            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item != null)
            {
                _context.Items.Remove(item);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("index");
        }
    }
}
