using CinemaTicketsDomain.DomainModels;
using CinemaTicketsRepository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CinemaTicketsRepository.Implementation; 

public class ShoppingCartRepository : IShoppingCartRepository{
    private readonly ApplicationDbContext context;
    private DbSet<ShoppingCart> entities;
    string errorMessage = string.Empty;

    public ShoppingCartRepository(ApplicationDbContext context) {
        this.context = context;
        this.entities = context.Set<ShoppingCart>();;
    }

    public ShoppingCart FetchShoppingCart(string userId) {
        return this.entities
            .Include(s => s.Tickets)
            .Include(s => s.CustomUser)
            .Include("Tickets.MovieProjection")
            .Include("Tickets.MovieProjection.Movie")
            .SingleOrDefault(s => s.CustomUser.Id.Equals(userId));
    }
}