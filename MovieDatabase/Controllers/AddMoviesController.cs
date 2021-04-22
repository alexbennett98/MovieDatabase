using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieDatabase.Models;

//This is the controller that takes place of the default controller
namespace MovieDatabase.Controllers
{
    public class AddMoviesController : Controller
    {
        private readonly MovieDbContext _context;

        public AddMoviesController(MovieDbContext context)
        {
            _context = context;
        }

        // GET: AddMovies
        public async Task<IActionResult> Index()
        {
            return View(await _context.AddMovie.ToListAsync());
        }

        // GET: AddMovies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var addMovie = await _context.AddMovie
                .FirstOrDefaultAsync(m => m.MovieId == id);
            if (addMovie == null)
            {
                return NotFound();
            }

            return View(addMovie);
        }
        public IActionResult MyPodcasts()
        {
            return View();
        }

        // GET: AddMovies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AddMovies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MovieId,Category,Title,YearReleased,DirectorName,MovieRating,IsEdited,IsLentTo,Notes")] AddMovie addMovie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(addMovie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(addMovie);
        }

        // GET: AddMovies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var addMovie = await _context.AddMovie.FindAsync(id);
            if (addMovie == null)
            {
                return NotFound();
            }
            return View(addMovie);
        }

        // POST: AddMovies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MovieId,Category,Title,YearReleased,DirectorName,MovieRating,IsEdited,IsLentTo,Notes")] AddMovie addMovie)
        {
            if (id != addMovie.MovieId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(addMovie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AddMovieExists(addMovie.MovieId))
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
            return View(addMovie);
        }

        // GET: AddMovies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var addMovie = await _context.AddMovie
                .FirstOrDefaultAsync(m => m.MovieId == id);
            if (addMovie == null)
            {
                return NotFound();
            }

            return View(addMovie);
        }

        // POST: AddMovies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var addMovie = await _context.AddMovie.FindAsync(id);
            _context.AddMovie.Remove(addMovie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AddMovieExists(int id)
        {
            return _context.AddMovie.Any(e => e.MovieId == id);
        }
    }
}
