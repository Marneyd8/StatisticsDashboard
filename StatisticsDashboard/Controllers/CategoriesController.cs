using Microsoft.AspNetCore.Mvc;
using StatisticsDashboard.Data;
using StatisticsDashboard.Models;

namespace StatisticsDashboard.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly MyAppContext _context;

        public CategoriesController(MyAppContext context)
        {
            _context = context;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id, Name")] Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Items");
            }
            return View(category);
        }
    }
}
