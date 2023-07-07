using System.Security.Claims;
using CinemaTicketServices.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CinemaTicketsApp.Controllers {
    public class ShoppingCartController : Controller {
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController(IShoppingCartService shoppingCartService) {
            _shoppingCartService = shoppingCartService;
        }


        // GET: ShoppingCart
        [Authorize]
        public IActionResult Index() {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View(_shoppingCartService.FetchShoppingCart(userId));
        }

        [HttpPost]
        [Authorize]
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
    }
}