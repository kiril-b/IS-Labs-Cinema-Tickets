using System.ComponentModel.DataAnnotations;

namespace CinemaTicketsApp.Models.Domain;

public class Movie {
    [Key] 
    public Guid Id { get; set; }
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