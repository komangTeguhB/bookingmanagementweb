using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EAP.Booking.Api.Models
{
    [Table("Client")]
    public class ClientEntity
    {
        public ClientEntity()
        {
            this.SessionsBookedData = new HashSet<SessionsBookedEntity>();
        }

        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("Name")]
        [StringLength(100)]
        public string Name { get; set; }
        [Column("Email")]
        [StringLength(100)]
        public string Email { get; set; }
        [Column("Phone")]
        public decimal Phone { get; set; }
        [Column("Address")]
        [StringLength(200)]
        public string Address { get; set; }
        [Column("Status")]
        public int Status { get; set; }

        public ICollection<SessionsBookedEntity> SessionsBookedData { get; set; }
    }
}