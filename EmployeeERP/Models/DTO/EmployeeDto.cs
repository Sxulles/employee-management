namespace EmployeeERP.Models.DTO
{
    public class EmployeeDto
    {
        public Guid? Id { get; init; }
        public string Name { get; init; }
        public DateOnly DateOfBirth { get; init; }
    }
}
