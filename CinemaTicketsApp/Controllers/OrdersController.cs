using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using CinemaTicketServices.Interface;
using DocumentFormat.OpenXml.Drawing;
using GemBox.Document;
using Microsoft.AspNetCore.Authorization;

namespace CinemaTicketsApp.Controllers {
    public class OrdersController : Controller {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService) {
            _orderService = orderService;
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");
        }

        // GET: Orders
        public IActionResult Index() {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View(_orderService.GetAllOrdersForUser(userId));
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateOrder() {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _orderService.PlaceOrder(userId);
            return Redirect("Index");
        }

        public FileContentResult CreateOrderInvoice(Guid id) {
            var stream = _orderService.CreateOrderInvoice(id);
            return File(stream.ToArray(), new PdfSaveOptions().ContentType, "OrderInvoice.pdf");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult ExportTickets() {
            return View(_orderService.GetUniqueGenres());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public FileContentResult ExportTicketsByGenre(string genre) {
            string fileName = "Orders.xlsx";
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            var fileContent = _orderService.ExportToExcel(genre);

            return File(fileContent, contentType, fileName);
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