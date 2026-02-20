using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mission06_Trotter.Models;

namespace Mission06_Trotter.Controllers
{
public class HomeController : Controller
    {
        private readonly MoviesContext _context;

        public HomeController(MoviesContext context)
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

        public IActionResult PrintTable()
        {
            var movies = _context.Movies
                .Include(m => m.Category)
                .OrderByDescending(m => m.MovieId)
                .ToList();
            return View(movies);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var movie = _context.Movies.Find(id);
            if (movie == null) return RedirectToAction("PrintTable");
            PopulateCategories();
            return View("AddMovie",movie);
        }
        
       [HttpPost]
        public IActionResult Edit(Movie updatedMovie)
        {
            if (!ModelState.IsValid)
            {
                PopulateCategories();
                return View("AddMovie", updatedMovie);
            }

            NormalizeOptionalStrings(updatedMovie);
            _context.Movies.Update(updatedMovie);
            _context.SaveChanges();
            return RedirectToAction("PrintTable");
        }
        
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var movie = _context.Movies
                .Include(m => m.Category)
                .FirstOrDefault(m => m.MovieId == id);
            if (movie == null) return RedirectToAction("PrintTable");
            return View("DeletePage",movie);
        }
        
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var movie = _context.Movies.Find(id);
            if (movie == null) return RedirectToAction("PrintTable");

            _context.Movies.Remove(movie);
            _context.SaveChanges();
            return RedirectToAction("PrintTable");
        }
        
       public IActionResult CancelDelete()
       {
           return RedirectToAction("PrintTable");
       }

        [HttpGet]
        public IActionResult AddMovie()
        {
            PopulateCategories();
            return View(new Movie());
        }

        [HttpPost]
        public IActionResult AddMovie(Movie movie)
        {
            if (ModelState.IsValid)
            {
                NormalizeOptionalStrings(movie);
                _context.Movies.Add(movie);
                _context.SaveChanges();
                return RedirectToAction("Confirmation", new { id = movie.MovieId });
            }

            PopulateCategories();
            return View(movie);
        }

        public IActionResult Confirmation(int id)
        {
            var movie = _context.Movies
                .Include(m => m.Category)
                .FirstOrDefault(m => m.MovieId == id);

            if (movie == null) return RedirectToAction("PrintTable");
            return View(movie);
        }

        private void PopulateCategories()
        {
            ViewBag.Categories = _context.Categories
                .OrderBy(c => c.CategoryName)
                .Select(c => new SelectListItem
                {
                    Value = c.CategoryId.ToString(),
                    Text = c.CategoryName
                })
                .ToList();
        }

        private static void NormalizeOptionalStrings(Movie movie)
        {
            movie.Director ??= string.Empty;
            movie.Rating ??= string.Empty;
            movie.LentTo ??= string.Empty;
        }
    }
}
