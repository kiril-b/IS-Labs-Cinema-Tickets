using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaTicketsDomain.DomainModels;

public class TicketInOrder : BaseEntity  {
    [Required] 
    public int Quantity { get; set; }

    // FKs
    public Guid MovieProjectionId { get; set; }
    [ForeignKey("MovieProjectionId")]
    public MovieProjection MovieProjection { get; set; }

    public Guid OrderId { get; set; }
    [ForeignKey("OrderId")]
    public Order Order { get; set; }
}