using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CinemaTicketsApp.Models.Identity;

namespace CinemaTicketsApp.Models.Domain;

public class Order {
    [Key] 
    public Guid Id { get; set; }
    public DateTime TimeCreated { get; set; } = DateTime.Now;

    // FKs
    public string CustomUserId { get; set; }
    [ForeignKey("CustomUserId")]
    public CustomUser CustomUser { get; set; }

    public virtual ICollection<TicketInOrder> Tickets { get; set; }
}