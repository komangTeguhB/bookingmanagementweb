using EAP.Booking.Api.Models;

namespace EAP.Booking.Api.DataAccess.Interface
{
    interface IStaffSessionRepository : IGenericRepository<StaffSessionEntity>
    {
        bool StaffSessionExists(int id);
    }
}
