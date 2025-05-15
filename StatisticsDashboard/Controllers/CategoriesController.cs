using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        [ValidateAntiForgeryToken]
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

        public async Task<IActionResult> Delete()
        {
            ViewBag.Categories = await _context.Categories.ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var categoryToDelete = _context.Categories.FirstOrDefault(x => x.Id == id);
            if (categoryToDelete != null)
            {
                _context.Categories.Remove(categoryToDelete);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index", "Items");
        }
    }
}
