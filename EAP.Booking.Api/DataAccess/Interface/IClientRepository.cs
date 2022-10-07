using EAP.Booking.Api.Models;

namespace EAP.Booking.Api.DataAccess.Interface
{
    interface IClientRepository: IGenericRepository<ClientEntity>
    {
        bool ClientExists(int id);
    }
}
