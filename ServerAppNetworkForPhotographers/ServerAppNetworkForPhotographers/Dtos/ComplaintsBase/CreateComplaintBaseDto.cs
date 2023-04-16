using System.ComponentModel.DataAnnotations;

namespace ServerAppNetworkForPhotographers.Dtos.Complaints
{
    public class CreateComplaintBaseDto
    {
        [Required]
        public string Name { get; set; }
    }
}
