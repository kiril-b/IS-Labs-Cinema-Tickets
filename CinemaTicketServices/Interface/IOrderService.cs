using CinemaTicketsDomain.DomainModels;

namespace CinemaTicketServices.Interface; 

public interface IOrderService {
    IEnumerable<Order> GetAllOrders();
    Order GetOrder(Guid? id);
    void PlaceOrder(Order entity);
}