using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace EmployeeERP.Models.DbEntities
{
    public class Employee : IdentityUser<Guid>, IEntity
    {
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public ICollection<TimeOffRequest>? TimeOffRequests { get; set; }
    }
}
