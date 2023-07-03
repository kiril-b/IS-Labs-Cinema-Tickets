using CinemaTicketsApp.Models.Domain;
using Microsoft.AspNetCore.Identity;

namespace CinemaTicketsApp.Models.Identity;

public class CustomUser : IdentityUser {
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Address { get; set; }

    public virtual ShoppingCart ShoppingCart { get; set; }
}