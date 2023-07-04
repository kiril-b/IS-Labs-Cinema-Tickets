using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaTicketsDomain.DomainModels;

public class TicketInShoppingCart : BaseEntity  {
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