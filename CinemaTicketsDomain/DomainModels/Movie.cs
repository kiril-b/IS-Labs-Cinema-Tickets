using System.ComponentModel.DataAnnotations;

namespace CinemaTicketsDomain.DomainModels;

public class Movie : BaseEntity {
    [Required] 
    public string Name { get; set; }
    [Required] 
    public string Description { get; set; }
    [Required] 
    public string Genre { get; set; }
    [Required] 
    public int Duration { get; set; }
    [Required] 
    public string Image { get; set; }
}