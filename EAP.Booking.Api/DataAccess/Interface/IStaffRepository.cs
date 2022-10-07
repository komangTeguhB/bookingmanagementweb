using EAP.Booking.Api.Models;

namespace EAP.Booking.Api.DataAccess.Interface
{
    interface IStaffRepository: IGenericRepository<StaffEntity>
    {
        bool StaffExists(int id);
    }
}
