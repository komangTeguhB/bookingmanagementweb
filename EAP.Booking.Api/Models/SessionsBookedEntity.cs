using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EAP.Booking.Api.Models
{
    [Table("SessionsBooked")]
    public class SessionsBookedEntity
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("Client")]
        public int Client { get; set; }
        [Column("StaffSession")]
        public int StaffSession { get; set; }
        [Column("IsActive")]
        public int IsActive { get; set; }

        [ForeignKey("StaffSession")]
        public virtual StaffSessionEntity StaffSessionData { get; set; }
        [ForeignKey("Client")]
        public virtual ClientEntity ClientData { get; set; }
    }
}