using System.ComponentModel.DataAnnotations;

namespace ServerAppNetworkForPhotographers.Models.Data.Dtos.Contents
{
    public class NewsDto
    {
        [Range(1, int.MaxValue)]
        public int PhotographerId { get; set; }
        public string? TypeContent { get; set; }
        public int[]? CategoriesIds { get; set; }
    }
}
