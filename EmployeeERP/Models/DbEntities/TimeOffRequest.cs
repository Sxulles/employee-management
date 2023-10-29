using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeERP.Models.DbEntities
{
    public class TimeOffRequest : IEntity
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public DateTime TimeOffStart { get; set; }
        [Required]
        public DateTime TimeOffEnd { get; set; }
        [Required]
        public DateTime DateOfRequest { get; init; } = DateTime.UtcNow;
        [Required]
        public string? Description { get; set; }
        [Required]
        public int Status { get; set; } = 0;
        [Required]
        public Guid EmployeeId { get; set; }
        [Required]
        [ForeignKey(nameof(EmployeeId))]
        public Employee? Employee { get; set; }
    }
}
