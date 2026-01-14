using System.ComponentModel.DataAnnotations;

namespace WebApplicationMosaicMPA101.ViewModels.EmployeeViewModels
{
    public class EmployeeUpdateVM
    {
        public int Id { get; set; }
        [Required, MaxLength(256), MinLength(3)]
        public string FirstName { get; set; } = string.Empty;
        [Required, MaxLength(256), MinLength(3)]
        public string LastName { get; set; } = string.Empty;
        public IFormFile? Image { get; set; }
        [Required, MaxLength(1024), MinLength(3)]
        public string Description { get; set; } = string.Empty;
        [Required]
        public int PositionId { get; set; }
    }
}
