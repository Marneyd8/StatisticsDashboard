using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StatisticsDashboard.Data;
using StatisticsDashboard.Models;

namespace StatisticsDashboard.Controllers
{
    public class ClientsController : Controller
    {
        private readonly MyAppContext _context;

        public ClientsController(MyAppContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var clients = await _context.Clients
                .Include(c => c.ItemClients)
                .ToListAsync();
            return View(clients);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
            return RedirectToAction("index");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] Client client)
        {
            // Bind is only optional, it specifies what atributes to bind with Post request
            if (ModelState.IsValid)
            {
                _context.Clients.Add(client);
                await _context.SaveChangesAsync();
                return RedirectToAction("index");
            }
            return View(client);
        }

        public async Task<IActionResult> Profile(int id)
        {
            var client = await _context.Clients
                .Include(c => c.ItemClients)
                .ThenInclude(ic => ic.Item)
                .ThenInclude(i => i.Category)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (client == null) return NotFound();

            return View(client);
        }

        [HttpGet]
        public async Task<IActionResult> AssignItem(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client == null) return NotFound();

            ViewBag.ClientId = id;
            ViewBag.Items = await _context.Items.ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignItem(int clientId, int itemId)
        {
            bool exists = await _context.ItemClients
                .AnyAsync(ic => ic.ClientId == clientId && ic.ItemId == itemId);
            if (!exists)
            {
                _context.ItemClients.Add(new ItemClient { ClientId = clientId, ItemId = itemId });
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Profile", new { id = clientId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveAssignedItem(int clientId, int itemId)
        {
            var client = await _context.Clients
                .Include(c => c.ItemClients)
                .FirstOrDefaultAsync(c => c.Id == clientId);

            if (client == null) return NotFound();

            var itemClient = client.ItemClients?.FirstOrDefault(ic => ic.ItemId == itemId);
            if (itemClient != null)
            {
                _context.ItemClients.Remove(itemClient);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Profile", new { id = clientId });
        }
    }
}
