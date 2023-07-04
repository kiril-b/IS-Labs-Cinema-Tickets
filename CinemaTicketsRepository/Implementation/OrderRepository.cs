using CinemaTicketsDomain.DomainModels;
using CinemaTicketsRepository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CinemaTicketsRepository.Implementation;

public class OrderRepository : IOrderRepository {
    private readonly ApplicationDbContext context;
    private DbSet<Order> entities;
    string errorMessage = string.Empty;

    public OrderRepository(ApplicationDbContext context) {
        this.context = context;
        this.entities = context.Set<Order>();
    }

    public IEnumerable<Order> GetAll() {
        return this.entities
            .Include(o => o.Tickets)
            .Include("Tickets.MovieProjection")
            .Include("Tickets.MovieProjection.Movie")
            .Include(o => o.CustomUser)
            .AsEnumerable();
    }

    public Order Get(Guid? id) {
        return this.entities
            .Include(o => o.Tickets)
            .Include("Tickets.MovieProjection")
            .Include("Tickets.MovieProjection.Movie")
            .Include(o => o.CustomUser)
            .SingleOrDefault(o => o.Id.Equals(id));
    }

    public void Insert(Order entity) {
        if (entity == null) {
            throw new ArgumentNullException("entity");
        }

        entities.Add(entity);
        context.SaveChanges();
    }
}