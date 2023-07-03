using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaTicketsApp.Models.Domain;

public class TicketInShoppingCart {
    [Key] 
    public Guid Id { get; set; }
    [Required] 
    public int Quantity { get; set; }

    // FKs
    public Guid MovieProjectionId { get; set; }
    [ForeignKey("MovieProjectionId")] 
    public MovieProjection MovieProjection { get; set; }

    public Guid ShoppingCartId { get; set; }
    [ForeignKey("ShoppingCartId")] 
    public ShoppingCart ShoppingCart { get; set; }
}