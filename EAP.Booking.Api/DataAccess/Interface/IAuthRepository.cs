using EAP.Booking.Api.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Threading.Tasks;

namespace EAP.Booking.Api.DataAccess.Interface
{
    interface IAuthRepository : IDisposable
    {
        Task<IdentityResult> RegisterUser(UserEntity userModel);
        Task<IdentityUser> FindUser(string userName, string password);
    }
}
