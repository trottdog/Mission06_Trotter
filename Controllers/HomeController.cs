using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Mission06_Trotter.Models;

namespace Mission06_Trotter.Controllers
{
public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetToKnowJoel()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddMovie()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddMovie(Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Movies.Add(movie);
                _context.SaveChanges();
                return RedirectToAction("Confirmation");
            }

            return View(movie);
        }

        public IActionResult Confirmation()
        {
            return View();
        }
    }
}