using System.ComponentModel.DataAnnotations;

namespace EmployeeClientAppNetworkForPhotographers.Models.Data.Dtos.ComplaintsBase
{
    public class CreateComplaintBaseDto
    {
        [Required]
        [StringLength(256, MinimumLength = 4)]
        public string Name { get; set; }
    }
}
