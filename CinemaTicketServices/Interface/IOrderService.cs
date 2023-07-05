using CinemaTicketsDomain.DomainModels;
using Microsoft.AspNetCore.Mvc;

namespace CinemaTicketServices.Interface; 

public interface IOrderService {
    IEnumerable<Order> GetAllOrders();
    IEnumerable<Order> GetAllOrdersForUser(string userId);
    Order GetOrder(Guid? id);
    void PlaceOrder(string userId);
    MemoryStream CreateOrderInvoice(Guid orderId);
    IEnumerable<string> GetUniqueGenres();
    byte[] ExportToExcel(string genre);
}