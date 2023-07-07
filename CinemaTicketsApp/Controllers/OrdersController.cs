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
        [Authorize]
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

        public IActionResult Checkout() {
            return View();
        }
    }
}