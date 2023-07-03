using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CinemaTicketsApp.Models.Domain;

public class TicketInOrder {
    [Key] 
    public Guid Id { get; set; }
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