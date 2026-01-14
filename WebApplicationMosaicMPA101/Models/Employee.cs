using WebApplicationMosaicMPA101.Models.Common;

namespace WebApplicationMosaicMPA101.Models
{
    public class Employee : BaseEntity
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int PositionId { get; set; }
        public Position Position { get; set; }
    }
}
