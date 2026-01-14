using System.ComponentModel.DataAnnotations;

namespace WebApplicationMosaicMPA101.ViewModels.PositionViewModels
{
    public class PositionCreateVM
    {
        [Required,MaxLength(256),MinLength(3)]
        public string Name { get; set; } = string.Empty;
    }
}
