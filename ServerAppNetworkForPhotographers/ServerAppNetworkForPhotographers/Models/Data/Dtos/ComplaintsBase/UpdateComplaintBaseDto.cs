using System.ComponentModel.DataAnnotations;

namespace ServerAppNetworkForPhotographers.Models.Data.Dtos.ComplaintsBase
{
    public class UpdateComplaintBaseDto
    {
        [Range(0, int.MaxValue)]
        public int Id { get; set; }

        [Required]
        [StringLength(256, MinimumLength = 4)]
        public string Name { get; set; }
    }
}
