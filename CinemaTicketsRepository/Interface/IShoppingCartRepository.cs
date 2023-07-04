using CinemaTicketsDomain.DomainModels;

namespace CinemaTicketsRepository.Interface; 

public interface IShoppingCartRepository {
    
    ShoppingCart FetchShoppingCart(string userId);
    
}