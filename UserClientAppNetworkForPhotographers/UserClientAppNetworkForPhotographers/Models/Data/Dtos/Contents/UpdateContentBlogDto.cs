using System.ComponentModel.DataAnnotations;

namespace UserClientAppNetworkForPhotographers.Models.Data.Dtos.Contents
{
    public class UpdateContentBlogDto
    {
        [Range(1, int.MaxValue)]
        public int Id { get; set; }

        [Required]
        [MinLength(4)]
        public string BlogBody { get; set; }
    }
}
