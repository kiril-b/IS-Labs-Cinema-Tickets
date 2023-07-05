using CinemaTicketsDomain.DomainModels;
using CinemaTicketServices.Interface;
using CinemaTicketsRepository.Interface;
using ClosedXML.Excel;
using GemBox.Document;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace CinemaTicketServices.Implementation;

public class OrderService : IOrderService {
    private readonly IOrderRepository _orderRepository;
    private readonly IUserRepository _userRepository;
    private readonly IRepository<TicketInOrder> _ticketsInOrderRepository;
    private readonly IMailService _mailService;

    public OrderService(IOrderRepository orderRepository, IUserRepository userRepository,
        IRepository<TicketInOrder> ticketsInOrderRepository, IMailService mailService) {
        _orderRepository = orderRepository;
        _userRepository = userRepository;
        _ticketsInOrderRepository = ticketsInOrderRepository;
        _mailService = mailService;
    }

    public IEnumerable<Order> GetAllOrders() {
        return _orderRepository.GetAll();
    }

    public IEnumerable<Order> GetAllOrdersForUser(string userId) {
        return _orderRepository
            .GetAll()
            .Where(o => o.CustomUserId.Equals(userId));
    }

    public Order GetOrder(Guid? id) {
        return _orderRepository.Get(id);
    }

    [HttpPost]
    public void PlaceOrder(string userId) {
        var user = _userRepository.Get(userId);

        // Create new order
        Order order = new Order() {
            Id = Guid.NewGuid(),
            CustomUserId = userId,
            CustomUser = user,
            TimeCreated = DateTime.Now
        };
        _orderRepository.Insert(order);

        // Create new tickets in order
        List<TicketInOrder> ticketsOrder = new List<TicketInOrder>();
        foreach (var ticketShoppingCart in user.ShoppingCart.Tickets) {
            TicketInOrder ticketInOrder = new TicketInOrder() {
                Id = Guid.NewGuid(),
                Order = order,
                OrderId = order.Id,
                Quantity = ticketShoppingCart.Quantity,
                MovieProjection = ticketShoppingCart.MovieProjection,
                MovieProjectionId = ticketShoppingCart.MovieProjectionId
            };
            ticketsOrder.Add(ticketInOrder);
        }

        foreach (var ticketInOrder in ticketsOrder) {
            _ticketsInOrderRepository.Insert(ticketInOrder);
        }


        // Send confirmation mail
        _mailService.SendEmail(
            user.Email,
            "Ticket Purchase Confirmation - Enjoy Your Movie!",
            "Thank you for choosing us for your movie ticket purchase! We are thrilled to confirm that your transaction was successful, and your ticket(s) are now reserved for the movie(s) of your choice."
        );
        
        // Clear shopping cart
        user.ShoppingCart.Tickets.Clear();
        _userRepository.Update(user);
    }

    public MemoryStream CreateOrderInvoice(Guid orderId) {
        var order = _orderRepository.Get(orderId);
        var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "OrderTemplate.docx");
        var document = DocumentModel.Load(templatePath);

        document.Content.Replace("{{OrderId}}", orderId.ToString());
        document.Content.Replace("{{User}}", order.CustomUser.Email.ToString());
        document.Content.Replace("{{OrderDateTime}}", order.TimeCreated.ToString());

        var tickets = new StringBuilder();
        float totalPrice = 0;

        foreach (var ticket in order.Tickets) {
            float ticketPrice = ticket.MovieProjection.PriceOfTicket;
            int quantity = ticket.Quantity;
            float ticketTotalPrice = ticketPrice * quantity;
            totalPrice += ticketTotalPrice;

            tickets.AppendLine(
                $"{ticket.MovieProjection.Movie.Name} | {ticket.MovieProjection.DateTime} | ${ticketPrice} | {quantity}");
        }

        document.Content.Replace("{{TicketsInOrder}}", tickets.ToString());
        document.Content.Replace("{{OrderTotal}}", totalPrice.ToString());

        var stream = new MemoryStream();
        document.Save(stream, new PdfSaveOptions());

        return stream;
    }

    public IEnumerable<string> GetUniqueGenres() {
        return this.GetAllOrders()
            .SelectMany(o => o.Tickets)
            .Select(t => t.MovieProjection.Movie.Genre)
            .Distinct()
            .AsEnumerable();
    }

    public byte[] ExportToExcel(string genre) {
        var tickets = this.GetAllOrders()
            .SelectMany(o => o.Tickets)
            .Where(t => t.MovieProjection.Movie.Genre.Equals(genre))
            .ToList();

        using (var workbook = new XLWorkbook()) {
            IXLWorksheet worksheet = workbook.Worksheets.Add("All Tickets");
            worksheet.Cell(1, 1).Value = "Ticket ID";
            worksheet.Cell(1, 2).Value = "User Email";
            worksheet.Cell(1, 3).Value = "Movie";
            worksheet.Cell(1, 4).Value = "Projection Date";
            worksheet.Cell(1, 5).Value = "OrderDate";
            worksheet.Cell(1, 6).Value = "Quantity";

            for (int i = 1; i <= tickets.Count(); i++) {
                var ticket = tickets[i - 1];
                worksheet.Cell(i + 1, 1).Value = ticket.Id.ToString();
                worksheet.Cell(i + 1, 2).Value = ticket.Order.CustomUser.Email;
                worksheet.Cell(i + 1, 3).Value = ticket.MovieProjection.Movie.Name;
                worksheet.Cell(i + 1, 4).Value = ticket.MovieProjection.DateTime;
                worksheet.Cell(i + 1, 5).Value = ticket.Order.TimeCreated;
                worksheet.Cell(i + 1, 6).Value = ticket.Quantity;
            }

            using (var stream = new MemoryStream()) {
                workbook.SaveAs(stream);
                return stream.ToArray();
            }
        }
    }
}