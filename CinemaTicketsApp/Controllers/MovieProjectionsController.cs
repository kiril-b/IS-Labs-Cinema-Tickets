using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CinemaTicketsApp.Data;
using CinemaTicketsApp.Models.Domain;

namespace CinemaTicketsApp.Controllers {
    public class MovieProjectionsController : Controller {
        private readonly ApplicationDbContext _context;

        public MovieProjectionsController(ApplicationDbContext context) {
            _context = context;
        }

        // GET: MovieProjections
        public async Task<IActionResult> Index() {
            var applicationDbContext = _context.MovieProjections.Include(m => m.Movie);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: MovieProjections/Details/5
        public async Task<IActionResult> Details(Guid? id) {
            if (id == null || _context.MovieProjections == null) {
                return NotFound();
            }

            var movieProjection = await _context.MovieProjections
                .Include(m => m.Movie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movieProjection == null) {
                return NotFound();
            }

            return View(movieProjection);
        }

        // GET: MovieProjections/Create
        public IActionResult Create() {
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Name");
            return View();
        }

        // POST: MovieProjections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,DateTime,AvailableTickets,PriceOfTicket,MovieId")] MovieProjection movieProjection) {
            if (ModelState.IsValid) {
                movieProjection.Id = Guid.NewGuid();
                _context.Add(movieProjection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Name", movieProjection.MovieId);
            return View(movieProjection);
        }

        // GET: MovieProjections/Edit/5
        public async Task<IActionResult> Edit(Guid? id) {
            if (id == null || _context.MovieProjections == null) {
                return NotFound();
            }

            var movieProjection = await _context.MovieProjections.FindAsync(id);
            if (movieProjection == null) {
                return NotFound();
            }

            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Description", movieProjection.MovieId);
            return View(movieProjection);
        }

        // POST: MovieProjections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,
            [Bind("Id,DateTime,AvailableTickets,PriceOfTicket,MovieId")] MovieProjection movieProjection) {
            if (id != movieProjection.Id) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(movieProjection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException) {
                    if (!MovieProjectionExists(movieProjection.Id)) {
                        return NotFound();
                    }
                    else {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Description", movieProjection.MovieId);
            return View(movieProjection);
        }

        // GET: MovieProjections/Delete/5
        public async Task<IActionResult> Delete(Guid? id) {
            if (id == null || _context.MovieProjections == null) {
                return NotFound();
            }

            var movieProjection = await _context.MovieProjections
                .Include(m => m.Movie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movieProjection == null) {
                return NotFound();
            }

            return View(movieProjection);
        }

        // POST: MovieProjections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id) {
            if (_context.MovieProjections == null) {
                return Problem("Entity set 'ApplicationDbContext.MovieProjections'  is null.");
            }

            var movieProjection = await _context.MovieProjections.FindAsync(id);
            if (movieProjection != null) {
                _context.MovieProjections.Remove(movieProjection);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieProjectionExists(Guid id) {
            return (_context.MovieProjections?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}