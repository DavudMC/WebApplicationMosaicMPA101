namespace WebApplicationMosaicMPA101.ViewModels.EmployeeViewModels
{
    public class EmployeeGetVM
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string PositionName { get; set; } = string.Empty;
    }
}
