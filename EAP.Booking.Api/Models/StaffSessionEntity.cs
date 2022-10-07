using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EAP.Booking.Api.Models
{
    [Table("StaffSession")]
    public class StaffSessionEntity
    {
        public StaffSessionEntity()
        {
            this.SessionsBookedData = new HashSet<SessionsBookedEntity>();
        }

        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("Date")]
        public DateTime Date { get; set; }
        [Column("Time")]
        public TimeSpan Time { get; set; }
        [Column("ConsultationType")]
        public int ConsultationType { get; set; }
        [Column("Staff")]
        public int Staff { get; set; }
        [Column("IsActive")]
        public int IsActive { get; set; }

        [ForeignKey("ConsultationType")]
        public virtual ConsultationTypeEntity ConsultationTypeData { get; set; }
        [ForeignKey("Staff")]
        public virtual StaffEntity StaffData { get; set; }

        public ICollection<SessionsBookedEntity> SessionsBookedData { get; set; }
    }
}