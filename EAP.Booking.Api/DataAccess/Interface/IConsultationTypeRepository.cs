using EAP.Booking.Api.Models;

namespace EAP.Booking.Api.DataAccess.Interface
{
    interface IConsultationTypeRepository : IGenericRepository<ConsultationTypeEntity>
    {
        bool ConsultationTypeExists(int id);
    }
}
