using System.ComponentModel.DataAnnotations;

namespace ServerAppNetworkForPhotographers.Models.Dtos.Complaints
{
    public class CreateComplaintDto
    {
        [StringLength(256, MinimumLength = 4)]
        public string? Text { get; set; }

        [Range(0, int.MaxValue)]
        public int ComplaintBaseId { get; set; }

        [Range(0, int.MaxValue)]
        public int ContentId { get; set; }
    }
}
