using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StatisticsDashboard.Data;
using StatisticsDashboard.Models;

namespace StatisticsDashboard.Controllers
{
    public class DashboardController : Controller
    {
        private readonly MyAppContext _context;
        
        public DashboardController(MyAppContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetItemStats()
        {
            var data = await _context.Items
                    .Include(i => i.Category)
                    .Where(i => i.Category != null)
                    .GroupBy(i => i.Category!.Name)
                    .Select(g => new
                    {
                        Category = g.Key,
                        TotalValue = g.Sum(i => i.Price),
                        Count = g.Count()
                    })
                    .ToListAsync();
            return Json(data);
        }

        [HttpGet("api/statistics/client-stats")]
        public async Task<IActionResult> GetClientStats()
        {
            var stats = await _context.Clients
                .Select(c => new ClientStatisticsDto
                {
                    ClientName = c.Name,
                    ItemCount = c.ItemClients.Count,
                    TotalValue = c.ItemClients.Sum(i => i.Item.Price)
                })
                .ToListAsync();
            return Ok(stats);
        }
    }
}
