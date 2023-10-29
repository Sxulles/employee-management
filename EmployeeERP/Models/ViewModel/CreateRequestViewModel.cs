namespace EmployeeERP.Models.ViewModel
{
    public class CreateRequestViewModel
    {
        public DateTime TimeOffStart { get; set; }
        public DateTime TimeOffEnd { get; set; }
        public string? Description { get; set; }
        public Guid EmployeeId { get; set; }
    }
}
