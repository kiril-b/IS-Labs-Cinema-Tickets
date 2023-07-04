using CinemaTicketsDomain.DomainModels;
using CinemaTicketServices.Interface;
using CinemaTicketsRepository.Interface;

namespace CinemaTicketServices.Implementation;

public class ShoppingCartService : IShoppingCartService {
    private readonly IShoppingCartRepository _shoppingCartRepository;
    private readonly IMovieProjectionRepository _movieProjectionRepository;
    private readonly IRepository<TicketInShoppingCart> _ticketInShoppingCartRepository;

    public ShoppingCartService(IShoppingCartRepository shoppingCartRepository,
        IMovieProjectionRepository movieProjectionRepository,
        IRepository<TicketInShoppingCart> ticketInShoppingCartRepository) {
        _shoppingCartRepository = shoppingCartRepository;
        _movieProjectionRepository = movieProjectionRepository;
        _ticketInShoppingCartRepository = ticketInShoppingCartRepository;
    }

    public ShoppingCart FetchShoppingCart(string userId) {
        return _shoppingCartRepository.FetchShoppingCart(userId);
    }

    public void AddTicketToShoppingCart(Guid movieProjectionId, int quantity, string userId) {
        var shoppingCart = this.FetchShoppingCart(userId);
        var movieProjection = _movieProjectionRepository.Get(movieProjectionId);

        if (movieProjection.AvailableTickets - quantity < 0) {
            quantity = movieProjection.AvailableTickets;
        }

        movieProjection.AvailableTickets -= quantity;

        if (this.TicketsForProjectionExist(movieProjection, shoppingCart)) {
            this.UpdateExistingTicket(quantity, shoppingCart, movieProjection);
        }
        else {
            this.CreateNewTicket(quantity, shoppingCart, movieProjection);
        }

        _movieProjectionRepository.Update(movieProjection);
    }

    public void RemoveTicketFromShoppingCart(Guid movieProjectionId, string userId) {
        var shoppingCart = this.FetchShoppingCart(userId);
        var ticketToRemove = this._ticketInShoppingCartRepository
            .GetAll()
            .SingleOrDefault(t => t.ShoppingCartId.Equals(shoppingCart.Id) &&
                                  t.MovieProjectionId.Equals(movieProjectionId));
        if (ticketToRemove != null) {
            var movieProjection = _movieProjectionRepository.Get(movieProjectionId);
            movieProjection.AvailableTickets += ticketToRemove.Quantity;
            this._ticketInShoppingCartRepository.Delete(ticketToRemove);
        }
    }

    private void UpdateExistingTicket(int quantity, ShoppingCart shoppingCart, MovieProjection movieProjection) {
        var ticket = _ticketInShoppingCartRepository
            .GetAll()
            .SingleOrDefault(t =>
                t.MovieProjectionId.Equals(movieProjection.Id) && t.ShoppingCartId.Equals(shoppingCart.Id));

        if (ticket != null) {
            ticket.Quantity += quantity;
            _ticketInShoppingCartRepository.Update(ticket);
        }
    }

    private bool TicketsForProjectionExist(MovieProjection movieProjection, ShoppingCart shoppingCart) {
        return _ticketInShoppingCartRepository
            .GetAll()
            .Any(t => t.MovieProjectionId.Equals(movieProjection.Id) && t.ShoppingCartId.Equals(shoppingCart.Id));
    }

    private void CreateNewTicket(int quantity, ShoppingCart shoppingCart, MovieProjection movieProjection) {
        TicketInShoppingCart ticketInShoppingCart = new TicketInShoppingCart {
            ShoppingCart = shoppingCart,
            ShoppingCartId = shoppingCart.Id,
            MovieProjection = movieProjection,
            MovieProjectionId = movieProjection.Id,
            Quantity = quantity,
        };

        _ticketInShoppingCartRepository.Insert(ticketInShoppingCart);
    }
}