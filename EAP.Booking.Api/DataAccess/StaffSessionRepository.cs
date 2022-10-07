using EAP.Booking.Api.DataAccess.Interface;
using EAP.Booking.Api.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace EAP.Booking.Api.DataAccess
{
    public class StaffSessionRepository : GenericRepository<EAPBMSContext, StaffSessionEntity>, IStaffSessionRepository
    {
        private readonly EAPBMSContext _db = new EAPBMSContext();

        public bool StaffSessionExists(int id)
        {
            return _db.StaffSessions.Count(e => e.Id == id) > 0;
        }

        //public Collection<StaffSessionEntity> GetStaffSessionView()
        //{

        //}
    }
}