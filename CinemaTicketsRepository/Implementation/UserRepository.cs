using CinemaTicketsDomain.Identity;
using Microsoft.EntityFrameworkCore;

namespace CinemaTicketsRepository.Implementation;

using CinemaTicketsRepository.Interface;

public class UserRepository : IUserRepository {
    private readonly ApplicationDbContext context;
    private DbSet<CustomUser> entities;
    string errorMessage = string.Empty;

    public UserRepository(ApplicationDbContext context) {
        this.context = context;
        entities = context.Set<CustomUser>();
    }

    public IEnumerable<CustomUser> GetAll() {
        return entities.AsEnumerable();
    }

    public CustomUser Get(string id) {
        //TODO: Test if this works
        return entities
            .Include(z => z.ShoppingCart)
            .Include("ShoppingCart.Tickets")
            .Include("ShoppingCart.Tickets.MovieProjection")
            .Include("ShoppingCart.Tickets.MovieProjection.Movie")
            .SingleOrDefault(s => s.Id == id);
    }

    public void Insert(CustomUser entity) {
        if (entity == null) {
            throw new ArgumentNullException("entity");
        }

        entities.Add(entity);
        context.SaveChanges();
    }

    public void Update(CustomUser entity) {
        if (entity == null) {
            throw new ArgumentNullException("entity");
        }

        entities.Update(entity);
        context.SaveChanges();
    }

    public void Delete(CustomUser entity) {
        if (entity == null) {
            throw new ArgumentNullException("entity");
        }

        entities.Remove(entity);
        context.SaveChanges();
    }
}