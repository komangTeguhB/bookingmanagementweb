using Microsoft.Owin;
using Owin;
using System.Data.Entity;
using BookingManagementWeb.Models;

[assembly: OwinStartupAttribute(typeof(BookingManagementWeb.Startup))]
namespace BookingManagementWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            // CreateDatabaseIfNotExists is the default initializer if not specified
            Database.SetInitializer<ApplicationDbContext>(new CreateDatabaseIfNotExists<ApplicationDbContext>());
        }
    }
}
