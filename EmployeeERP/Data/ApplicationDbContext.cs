using EmployeeERP.Models.DbEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;

namespace EmployeeERP.Data
{
    public class ApplicationDbContext : IdentityDbContext<Employee, IdentityRole<Guid>, Guid>
    {
        public DbSet<TimeOffRequest> TimeOffRequests { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var employees = new List<Employee>()
            {
                new Employee { Id = Guid.NewGuid(), UserName = "John Doe", DateOfBirth = new DateTime(1977, 11, 1) },
                new Employee { Id = Guid.NewGuid(), UserName = "Teszt Elek", DateOfBirth = new DateTime(2000, 05, 14) },
                new Employee { Id = Guid.NewGuid(), UserName = "Paris", DateOfBirth = new DateTime(1997, 06, 5) }
            };

            modelBuilder.Entity<Employee>().HasData(employees);

            foreach (var employee in employees)
            {
                var timeOffRequests = GenerateTimeoffRequests(Random.Shared.Next(1, 11), employee.Id);
                modelBuilder.Entity<TimeOffRequest>().HasData(timeOffRequests);
            }
        }

        private IEnumerable<TimeOffRequest> GenerateTimeoffRequests(int quantity, Guid employeeId)
        {
            for (int i = 0; i < quantity; i++)
            {
                yield return new TimeOffRequest
                {
                    Id = Guid.NewGuid(),
                    TimeOffStart = DateTime.UtcNow,
                    TimeOffEnd = DateTime.UtcNow.AddDays(Random.Shared.Next(1, 10)),
                    Description = "Lorem ipsum...",
                    Status = 0,
                    EmployeeId = employeeId,
                };
            }
        }
    }
}
