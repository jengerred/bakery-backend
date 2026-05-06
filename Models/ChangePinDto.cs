namespace BakeryBackend.Dtos
{
    public class ChangePinDto
    {
        public string EmployeeId { get; set; } = string.Empty;
        public string NewPin { get; set; } = string.Empty;

        // Role of the person making the request
        public string RequestedByRole { get; set; } = string.Empty;
    }
}
