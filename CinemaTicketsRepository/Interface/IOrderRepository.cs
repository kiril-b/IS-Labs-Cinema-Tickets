using CinemaTicketsDomain.DomainModels;

namespace CinemaTicketsRepository.Interface; 

public interface IOrderRepository {
    IEnumerable<Order> GetAll();
    Order Get(Guid? id);
    void Insert(Order entity);
}