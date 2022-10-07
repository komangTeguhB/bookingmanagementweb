using EAP.Booking.Api.DataAccess.Interface;
using EAP.Booking.Api.Models;
using System.Collections.Generic;
using System.Linq;

namespace EAP.Booking.Api.DataAccess
{
    public class ClientRepository : GenericRepository<EAPBMSContext, ClientEntity>, IClientRepository
    {
        private readonly EAPBMSContext _db = new EAPBMSContext();
        
        public bool ClientExists(int id)
        {
            return _db.Clients.Count(e => e.Id == id) > 0;
        }
        
    }
}