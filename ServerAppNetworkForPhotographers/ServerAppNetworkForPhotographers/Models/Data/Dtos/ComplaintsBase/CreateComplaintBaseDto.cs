using System.ComponentModel.DataAnnotations;

namespace ServerAppNetworkForPhotographers.Models.Data.Dtos.ComplaintsBase
{
    public class CreateComplaintBaseDto
    {
        [Required]
        [StringLength(64, MinimumLength = 4)]
        public string Name { get; set; }
    }
}
