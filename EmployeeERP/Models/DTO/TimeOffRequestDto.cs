namespace EmployeeERP.Models.DTO
{
    public class TimeOffRequestDto
    {
        public Guid? Id { get; set; }
        public DateOnly TimeOffStart { get; init; }
        public DateOnly TimeOffEnd { get; init; }
        public DateOnly DateOfRequest { get; set; }
        public string? Description { get; init; }
        public int Status { get; init; }
        public string EmployeeName { get; init; }
    }
}
