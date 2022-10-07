using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EAP.Booking.Api.Models
{
    [Table("ConsultationType")]
    public class ConsultationTypeEntity
    {
        public ConsultationTypeEntity()
        {
            this.StaffSessionsData = new HashSet<StaffSessionEntity>();
        }

        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("TypeName")]
        [StringLength(100)]
        public string TypeName { get; set; }
        [Column("Duration")]
        public int Duration { get; set; }

        public virtual ICollection<StaffSessionEntity> StaffSessionsData { get; set; }
    }
}