using EAP.Booking.Api.DataAccess.Interface;
using EAP.Booking.Api.Models;
using System.Linq;

namespace EAP.Booking.Api.DataAccess
{
    public class ConsultationTypeRepository : GenericRepository<EAPBMSContext, ConsultationTypeEntity>, IConsultationTypeRepository
    {
        private readonly EAPBMSContext _db = new EAPBMSContext();

        public bool ConsultationTypeExists(int id)
        {
            return _db.ConsultationTypes.Count(e => e.Id == id) > 0;
        }
    }
}