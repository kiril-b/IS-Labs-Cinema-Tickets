using CinemaTicketsDomain.DomainModels;

namespace CinemaTicketServices.Interface; 

public interface IShoppingCartService {
    ShoppingCart FetchShoppingCart(string userId);
    void AddTicketToShoppingCart(Guid movieProjectionId, int quantity, string userId);
    void RemoveTicketFromShoppingCart(Guid movieProjectionId, string userId);
}