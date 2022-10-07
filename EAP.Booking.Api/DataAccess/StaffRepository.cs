using EAP.Booking.Api.DataAccess.Interface;
using EAP.Booking.Api.Models;
using System.Linq;

namespace EAP.Booking.Api.DataAccess
{
    public class StaffRepository: GenericRepository<EAPBMSContext, StaffEntity>, IStaffRepository
    {
        private readonly EAPBMSContext _db = new EAPBMSContext();

        public bool StaffExists(int id)
        {
            return _db.Staffs.Count(e => e.Id == id) > 0;
        }
    }
}