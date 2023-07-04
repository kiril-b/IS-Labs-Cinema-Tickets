using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CinemaTicketsDomain.DomainModels;
using CinemaTicketServices.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CinemaTicketsApp.Controllers {
    public class MoviesController : Controller {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService) {
            _movieService = movieService;
        }

        // GET: Movies
        public IActionResult Index() {
            return View(_movieService.GetAllMovies());
        }

        // GET: Movies/Details/5
        public IActionResult Details(Guid? id) {
            if (id == null) {
                return NotFound();
            }

            var movie = this._movieService.GetMovieById(id);
            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create() {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,Description,Genre,Duration,Image")] Movie movie) {
            if (ModelState.IsValid) {
                this._movieService.CreateNewMovie(movie);
                return RedirectToAction(nameof(Index));
            }

            return View(movie);
        }

        // GET: Movies/Edit/5
        public IActionResult Edit(Guid? id) {
            if (id == null) {
                return NotFound();
            }

            var movie = this._movieService.GetMovieById(id);
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Id,Name,Description,Genre,Duration,Image")] Movie movie) {
            if (id != movie.Id) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    this._movieService.UpdateMovie(movie);
                }
                catch (DbUpdateConcurrencyException) {
                    if (!_movieService.MovieExists(movie.Id)) {
                        return NotFound();
                    }
                    else {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(movie);
        }

        // GET: Movies/Delete/5
        public IActionResult Delete(Guid? id) {
            if (id == null) {
                return NotFound();
            }
            var movie = this._movieService.GetMovieById(id);
            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id) {
            this._movieService.DeleteMovie(id);
            return RedirectToAction(nameof(Index));
        }
    }
}