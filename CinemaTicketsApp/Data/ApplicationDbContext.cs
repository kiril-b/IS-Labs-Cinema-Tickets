using CinemaTicketsApp.Models.Domain;
using CinemaTicketsApp.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CinemaTicketsApp.Data;

public class ApplicationDbContext : IdentityDbContext<CustomUser> {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }
    
    public virtual DbSet<Movie> Movies { get; set; }
    public virtual DbSet<MovieProjection> MovieProjections { get; set; }
    public virtual DbSet<Order> Orders { get; set; }
    public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }
    public virtual DbSet<TicketInShoppingCart> TicketInShoppingCarts { get; set; }
    public virtual DbSet<TicketInOrder> TicketInOrders { get; set; }
}