using System.ComponentModel.DataAnnotations;

namespace ServerAppNetworkForPhotographers.Models.Data.Dtos.Comments
{
    public class CreateCommentDto
    {
        [Required]
        [StringLength(512, MinimumLength = 4)]
        public string Text { get; set; }

        [Range(1, int.MaxValue)]
        public int PhotographerId { get; set; }

        [Range(1, int.MaxValue)]
        public int ContentId { get; set; }
    }
}
