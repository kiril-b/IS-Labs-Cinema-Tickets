using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CinemaTicketsDomain.Identity;

namespace CinemaTicketsDomain.DomainModels;

public class Order : BaseEntity  {
    public DateTime TimeCreated { get; set; } = DateTime.Now;

    // FKs
    public string CustomUserId { get; set; }
    [ForeignKey("CustomUserId")]
    public CustomUser CustomUser { get; set; }

    public virtual ICollection<TicketInOrder> Tickets { get; set; }
}