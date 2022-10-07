using EAP.Booking.Api.DataAccess.Interface;
using EAP.Booking.Api.Models;
using System.Linq;

namespace EAP.Booking.Api.DataAccess
{
    public class SessionsBookedRepository : GenericRepository<EAPBMSContext, SessionsBookedEntity>, ISessionsBookedRepository
    {
        private readonly EAPBMSContext _db = new EAPBMSContext();

        public bool SessionBookedExists(int id)
        {
            return _db.SessionsBooked.Count(e => e.Id == id) > 0;
        }
    }
}