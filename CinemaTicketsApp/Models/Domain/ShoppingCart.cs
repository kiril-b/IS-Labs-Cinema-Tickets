using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CinemaTicketsApp.Models.Identity;

namespace CinemaTicketsApp.Models.Domain;

public class ShoppingCart {
    [Key] 
    public Guid Id { get; set; }

    // FKs
    public string CustomUserId { get; set; }
    [ForeignKey("CustomUserId")]
    public CustomUser CustomUser { get; set; }

    public virtual ICollection<TicketInShoppingCart> Tickets { get; set; }
}