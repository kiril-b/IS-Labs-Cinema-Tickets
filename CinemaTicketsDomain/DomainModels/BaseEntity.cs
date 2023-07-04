using System.ComponentModel.DataAnnotations;

namespace CinemaTicketsDomain.DomainModels;

public class BaseEntity {
    [Key] public Guid Id { get; set; }
}