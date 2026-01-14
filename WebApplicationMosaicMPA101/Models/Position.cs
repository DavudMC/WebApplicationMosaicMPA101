using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using WebApplicationMosaicMPA101.Models.Common;

namespace WebApplicationMosaicMPA101.Models
{
    public class Position : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public ICollection<Employee> Employees { get; set; } = [];
    }
}
