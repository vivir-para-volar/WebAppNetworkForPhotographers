using System.ComponentModel.DataAnnotations;

namespace ServerAppNetworkForPhotographers.Models.Dtos.ComplaintsBase
{
    public class UpdateComplaintBaseDto
    {
        [Range(0, int.MaxValue)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
