using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CinemaTicketsDomain.DomainModels;
using CinemaTicketServices.Implementation;
using CinemaTicketServices.Interface;

namespace CinemaTicketsApp.Controllers {
    public class MovieProjectionsController : Controller {
        private readonly IMovieProjectionService _movieProjectionService;
        private readonly IMovieService _movieService;

        public MovieProjectionsController(IMovieProjectionService movieProjectionService, IMovieService movieService) {
            _movieProjectionService = movieProjectionService;
            _movieService = movieService;
        }

        // GET: MovieProjections
        public IActionResult Index() {
            return View(_movieProjectionService.GetAllMovieProjections());
        }

        // GET: MovieProjections/Details/5
        public IActionResult Details(Guid? id) {
            if (id == null) {
                return NotFound();
            }

            var movieProjection = this._movieProjectionService.GetMovieProjectionById(id);
            return View(movieProjection);
        }

        // GET: MovieProjections/Create
        public IActionResult Create() {
            ViewData["MovieId"] = new SelectList(_movieService.GetAllMovies(), "Id", "Name");
            return View();
        }

        // POST: MovieProjections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(
            [Bind("Id,DateTime,AvailableTickets,PriceOfTicket,MovieId")] MovieProjection movieProjection) {
            if (ModelState.IsValid) {
                _movieProjectionService.CreateNewMovieProjection(movieProjection);
                return RedirectToAction(nameof(Index));
            }

            ViewData["MovieId"] = new SelectList(_movieService.GetAllMovies(), "Id", "Name", movieProjection.MovieId);
            return View(movieProjection);
        }

        // GET: MovieProjections/Edit/5
        public IActionResult Edit(Guid? id) {
            if (id == null) {
                return NotFound();
            }

            var movieProjection = _movieProjectionService.GetMovieProjectionById(id);
            if (movieProjection == null) {
                return NotFound();
            }

            ViewData["MovieId"] =
                new SelectList(_movieService.GetAllMovies(), "Id", "Name", movieProjection.MovieId);
            return View(movieProjection);
        }

        // POST: MovieProjections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,
            [Bind("Id,DateTime,AvailableTickets,PriceOfTicket,MovieId")]
            MovieProjection movieProjection) {
            if (id != movieProjection.Id) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _movieProjectionService.UpdateMovieProjection(movieProjection);
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

            ViewData["MovieId"] =
                new SelectList(_movieService.GetAllMovies(), "Id", "Description", movieProjection.MovieId);
            return View(movieProjection);
        }

        // GET: MovieProjections/Delete/5
        public IActionResult Delete(Guid? id) {
            if (id == null) {
                return NotFound();
            }

            var movieProjection = _movieProjectionService.GetMovieProjectionById(id);
            if (movieProjection == null) {
                return NotFound();
            }

            return View(movieProjection);
        }

        // POST: MovieProjections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id) {
            _movieProjectionService.DeleteMovieProjection(id);
            return RedirectToAction(nameof(Index));
        }

        private bool MovieProjectionExists(Guid id) {
            return _movieProjectionService.MovieProjectionExists(id);
        }
    }
}