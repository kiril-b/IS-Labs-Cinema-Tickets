using CinemaTicketsDomain.DomainModels;
using CinemaTicketServices.Interface;
using CinemaTicketsRepository.Interface;

namespace CinemaTicketServices.Implementation;

public class OrderService : IOrderService {
    private readonly IOrderRepository _orderRepository;

    public OrderService(IOrderRepository orderRepository) {
        _orderRepository = orderRepository;
    }
    
    public IEnumerable<Order> GetAllOrders() {
        return _orderRepository.GetAll();
    }

    public Order GetOrder(Guid? id) {
        return _orderRepository.Get(id);
    }

    public void PlaceOrder(Order order) {
        // TODO: Change this method to accept user ID and create the order here
        _orderRepository.Insert(order);
    }
}