using System.ComponentModel.DataAnnotations;

namespace ServerAppNetworkForPhotographers.Models.Data.Dtos.Complaints
{
    public class CreateComplaintDto
    {
        [StringLength(512, MinimumLength = 16)]
        public string? Text { get; set; }

        [Range(1, int.MaxValue)]
        public int ComplaintBaseId { get; set; }

        [Range(1, int.MaxValue)]
        public int ContentId { get; set; }
    }
}
