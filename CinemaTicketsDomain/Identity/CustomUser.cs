using CinemaTicketsDomain.DomainModels;
using Microsoft.AspNetCore.Identity;

namespace CinemaTicketsDomain.Identity;

public class CustomUser : IdentityUser {
    // public string FirstName { get; set; }
    //
    // public string LastName { get; set; }
    //
    // public string Address { get; set; }

    public virtual ShoppingCart ShoppingCart { get; set; }
}