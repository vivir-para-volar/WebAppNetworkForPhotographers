using System.ComponentModel.DataAnnotations;

namespace ServerAppNetworkForPhotographers.Dtos.Complaints
{
    public class CreateComplaintDto
    {
        [Required]
        public string Text { get; set; }
    }
}
