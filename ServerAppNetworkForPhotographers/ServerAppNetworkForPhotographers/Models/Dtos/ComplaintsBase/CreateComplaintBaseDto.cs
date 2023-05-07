using System.ComponentModel.DataAnnotations;

namespace ServerAppNetworkForPhotographers.Models.Dtos.ComplaintsBase
{
    public class CreateComplaintBaseDto
    {
        [Required]
        public string Name { get; set; }
    }
}
