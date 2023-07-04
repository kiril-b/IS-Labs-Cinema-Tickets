using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CinemaTicketsDomain.DomainModels;
using CinemaTicketServices.Interface;

namespace CinemaTicketsApp.Controllers {
    public class OrdersController : Controller {
        private readonly IOrderService _orderService;
        
        public OrdersController(IOrderService orderService) {
            _orderService = orderService;
        }

        
        // TODO: Manage roles
        
        // GET: Orders
        public IActionResult Index() {
            return View(_orderService.GetAllOrders());
        }

        // GET: Orders/Details/5
        public IActionResult Details(Guid? id) {
            if (id == null) {
                return NotFound();
            }

            var movie = this._orderService.GetOrder(id);
            return View(movie);
        }

        // GET: Orders/Create
        // TODO: This should not exist
        public IActionResult Create() {
            // ViewData["CustomUserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,TimeCreated,CustomUserId")] Order order) {
            // if (ModelState.IsValid) {
            //     order.Id = Guid.NewGuid();
            //     _context.Add(order);
            //     await _context.SaveChangesAsync();
            //     return RedirectToAction(nameof(Index));
            // }
            //
            // ViewData["CustomUserId"] = new SelectList(_context.Users, "Id", "Id", order.CustomUserId);
            // return View(order);
            return null;
        }

        // GET: Orders/Edit/5
        // public async Task<IActionResult> Edit(Guid? id) {
        //     if (id == null || _context.Orders == null) {
        //         return NotFound();
        //     }
        //
        //     var order = await _context.Orders.FindAsync(id);
        //     if (order == null) {
        //         return NotFound();
        //     }
        //
        //     ViewData["CustomUserId"] = new SelectList(_context.Users, "Id", "Id", order.CustomUserId);
        //     return View(order);
        // }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Edit(Guid id, [Bind("Id,TimeCreated,CustomUserId")] Order order) {
        //     if (id != order.Id) {
        //         return NotFound();
        //     }
        //
        //     if (ModelState.IsValid) {
        //         try {
        //             _context.Update(order);
        //             await _context.SaveChangesAsync();
        //         }
        //         catch (DbUpdateConcurrencyException) {
        //             if (!OrderExists(order.Id)) {
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
        //     ViewData["CustomUserId"] = new SelectList(_context.Users, "Id", "Id", order.CustomUserId);
        //     return View(order);
        // }

        // GET: Orders/Delete/5
        // public async Task<IActionResult> Delete(Guid? id) {
        //     if (id == null || _context.Orders == null) {
        //         return NotFound();
        //     }
        //
        //     var order = await _context.Orders
        //         .Include(o => o.CustomUser)
        //         .FirstOrDefaultAsync(m => m.Id == id);
        //     if (order == null) {
        //         return NotFound();
        //     }
        //
        //     return View(order);
        // }

        // POST: Orders/Delete/5
        // [HttpPost, ActionName("Delete")]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> DeleteConfirmed(Guid id) {
        //     if (_context.Orders == null) {
        //         return Problem("Entity set 'ApplicationDbContext.Orders'  is null.");
        //     }
        //
        //     var order = await _context.Orders.FindAsync(id);
        //     if (order != null) {
        //         _context.Orders.Remove(order);
        //     }
        //
        //     await _context.SaveChangesAsync();
        //     return RedirectToAction(nameof(Index));
        // }
        
    }
}