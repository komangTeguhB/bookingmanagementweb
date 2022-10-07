using System.Data.Entity;

namespace EAP.Booking.Api.Models
{
    public class EAPBMSContext : DbContext
    {
        public EAPBMSContext() : base ("DefaultConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;

        }

        public DbSet<ClientEntity> Clients { get; set; }
        public DbSet<StaffEntity> Staffs{ get; set; }
        public DbSet<ConsultationTypeEntity> ConsultationTypes { get; set; }
        public DbSet<StaffSessionEntity> StaffSessions { get; set; }
        public DbSet<SessionsBookedEntity> SessionsBooked { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StaffSessionEntity>()
                      .HasRequired(s => s.ConsultationTypeData)
                      .WithMany(l => l.StaffSessionsData)
                      .HasForeignKey(c => c.ConsultationType);
            modelBuilder.Entity<StaffSessionEntity>()
                      .HasRequired(s => s.StaffData)
                      .WithMany(l => l.StaffSessionsData)
                      .HasForeignKey(c => c.Staff);
            modelBuilder.Entity<SessionsBookedEntity>()
                       .HasRequired(s => s.StaffSessionData)
                       .WithMany(l => l.SessionsBookedData)
                       .HasForeignKey(c => c.StaffSession);
            modelBuilder.Entity<SessionsBookedEntity>()
                        .HasRequired(s => s.ClientData)
                        .WithMany(l => l.SessionsBookedData)
                        .HasForeignKey(c => c.Client);

        }
    }
}