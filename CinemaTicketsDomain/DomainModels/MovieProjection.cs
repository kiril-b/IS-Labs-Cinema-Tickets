using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaTicketsDomain.DomainModels;

public class MovieProjection : BaseEntity {
    [Required] 
    public DateTime DateTime { get; set; }
    [Required] 
    public int AvailableTickets { get; set; }
    [Required] 
    public float PriceOfTicket { get; set; }

    // FKs
    public Guid MovieId { get; set; }
    [ForeignKey("MovieId")] 
    public Movie Movie { get; set; }
}