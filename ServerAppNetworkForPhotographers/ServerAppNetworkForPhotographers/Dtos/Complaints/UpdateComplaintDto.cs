using System.ComponentModel.DataAnnotations;

namespace ServerAppNetworkForPhotographers.Dtos.Complaints
{
    public class UpdateComplaintDto
    {
        [Range(0, int.MaxValue)]
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }
    }
}
