using System.ComponentModel.DataAnnotations;

namespace ServerAppNetworkForPhotographers.Models.Dtos.ComplaintsBase
{
    public class CreateComplaintBaseDto
    {
        [Required]
        [StringLength(256, MinimumLength = 4)]
        public string Name { get; set; }
    }
}
