using System.ComponentModel.DataAnnotations;

namespace ServerAppNetworkForPhotographers.Models.Data.Dtos.ComplaintsBase
{
    public class UpdateComplaintBaseDto
    {
        [Range(1, int.MaxValue)]
        public int Id { get; set; }

        [Required]
        [StringLength(64, MinimumLength = 4)]
        public string Name { get; set; }
    }
}
