using Microsoft.AspNet.Identity.EntityFramework;

namespace EAP.Booking.Api.Models
{
    public class AuthContext : IdentityDbContext<IdentityUser>
    {
        public AuthContext()
            : base("DefaultConnection")
        {

        }
    }
}