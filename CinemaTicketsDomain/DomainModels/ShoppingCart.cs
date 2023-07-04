using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CinemaTicketsDomain.Identity;

namespace CinemaTicketsDomain.DomainModels;

public class ShoppingCart : BaseEntity  {
    // FKs
    public string CustomUserId { get; set; }
    [ForeignKey("CustomUserId")]
    public CustomUser CustomUser { get; set; }

    public virtual ICollection<TicketInShoppingCart> Tickets { get; set; }
}