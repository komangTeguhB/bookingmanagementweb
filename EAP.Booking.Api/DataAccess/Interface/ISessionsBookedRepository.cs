using EAP.Booking.Api.Models;

namespace EAP.Booking.Api.DataAccess.Interface
{
    interface ISessionsBookedRepository : IGenericRepository<SessionsBookedEntity>
    {
        bool SessionBookedExists(int id);
    }
}
