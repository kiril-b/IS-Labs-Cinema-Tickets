using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CinemaTicketsDomain.DomainModels;
using CinemaTicketServices.Interface;
using CinemaTicketsRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CinemaTicketsApp.Controllers {
    public class ShoppingCartController : Controller {
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController(IShoppingCartService shoppingCartService) {
            _shoppingCartService = shoppingCartService;
        }

        //TODO: Da pobara da e najaven korisnikot za da mozi da gi pristapva ovie

        // GET: ShoppingCart
        public IActionResult Index() {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View(_shoppingCartService.FetchShoppingCart(userId));
        }

        [HttpPost]
        public IActionResult AddToShoppingCart(Guid movieProjectionId, int quantity) {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            _shoppingCartService.AddTicketToShoppingCart(movieProjectionId, quantity, userId);
            return Redirect("Index");
        }

        public IActionResult DeleteItemFromShoppingCart(Guid movieProjectionId) {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            _shoppingCartService.RemoveTicketFromShoppingCart(movieProjectionId, userId);
            return Redirect("Index");
        }
        

        // GET: ShoppingCart/Details/5
        // public async Task<IActionResult> Details(Guid? id) {
        //     if (id == null || _context.ShoppingCarts == null) {
        //         return NotFound();
        //     }
        //
        //     var shoppingCart = await _context.ShoppingCarts
        //         .Include(s => s.CustomUser)
        //         .FirstOrDefaultAsync(m => m.Id == id);
        //     if (shoppingCart == null) {
        //         return NotFound();
        //     }
        //
        //     return View(shoppingCart);
        // }
        //
        // // GET: ShoppingCart/Create
        // public IActionResult Create() {
        //     ViewData["CustomUserId"] = new SelectList(_context.Users, "Id", "Id");
        //     return View();
        // }
        //
        // // POST: ShoppingCart/Create
        // // To protect from overposting attacks, enable the specific properties you want to bind to.
        // // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Create([Bind("Id,CustomUserId")] ShoppingCart shoppingCart) {
        //     if (ModelState.IsValid) {
        //         shoppingCart.Id = Guid.NewGuid();
        //         _context.Add(shoppingCart);
        //         await _context.SaveChangesAsync();
        //         return RedirectToAction(nameof(Index));
        //     }
        //
        //     ViewData["CustomUserId"] = new SelectList(_context.Users, "Id", "Id", shoppingCart.CustomUserId);
        //     return View(shoppingCart);
        // }
        //
        // // GET: ShoppingCart/Edit/5
        // public async Task<IActionResult> Edit(Guid? id) {
        //     if (id == null || _context.ShoppingCarts == null) {
        //         return NotFound();
        //     }
        //
        //     var shoppingCart = await _context.ShoppingCarts.FindAsync(id);
        //     if (shoppingCart == null) {
        //         return NotFound();
        //     }
        //
        //     ViewData["CustomUserId"] = new SelectList(_context.Users, "Id", "Id", shoppingCart.CustomUserId);
        //     return View(shoppingCart);
        // }
        //
        // // POST: ShoppingCart/Edit/5
        // // To protect from overposting attacks, enable the specific properties you want to bind to.
        // // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Edit(Guid id, [Bind("Id,CustomUserId")] ShoppingCart shoppingCart) {
        //     if (id != shoppingCart.Id) {
        //         return NotFound();
        //     }
        //
        //     if (ModelState.IsValid) {
        //         try {
        //             _context.Update(shoppingCart);
        //             await _context.SaveChangesAsync();
        //         }
        //         catch (DbUpdateConcurrencyException) {
        //             if (!ShoppingCartExists(shoppingCart.Id)) {
        //                 return NotFound();
        //             }
        //             else {
        //                 throw;
        //             }
        //         }
        //
        //         return RedirectToAction(nameof(Index));
        //     }
        //
        //     ViewData["CustomUserId"] = new SelectList(_context.Users, "Id", "Id", shoppingCart.CustomUserId);
        //     return View(shoppingCart);
        // }
        //
        // // GET: ShoppingCart/Delete/5
        // public async Task<IActionResult> Delete(Guid? id) {
        //     if (id == null || _context.ShoppingCarts == null) {
        //         return NotFound();
        //     }
        //
        //     var shoppingCart = await _context.ShoppingCarts
        //         .Include(s => s.CustomUser)
        //         .FirstOrDefaultAsync(m => m.Id == id);
        //     if (shoppingCart == null) {
        //         return NotFound();
        //     }
        //
        //     return View(shoppingCart);
        // }
        //
        // // POST: ShoppingCart/Delete/5
        // [HttpPost, ActionName("Delete")]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> DeleteConfirmed(Guid id) {
        //     if (_context.ShoppingCarts == null) {
        //         return Problem("Entity set 'ApplicationDbContext.ShoppingCarts'  is null.");
        //     }
        //
        //     var shoppingCart = await _context.ShoppingCarts.FindAsync(id);
        //     if (shoppingCart != null) {
        //         _context.ShoppingCarts.Remove(shoppingCart);
        //     }
        //
        //     await _context.SaveChangesAsync();
        //     return RedirectToAction(nameof(Index));
        // }
        //
        // private bool ShoppingCartExists(Guid id) {
        //     return _context.ShoppingCarts.Any(e => e.Id == id);
        // }
    }
}