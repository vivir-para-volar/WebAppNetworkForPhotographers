using Microsoft.Build.Framework;

namespace ServerAppNetworkForPhotographers.Models.Data.Dtos.Contents
{
    public class OthersDto
    {
        [Required]
        public string TypeSorting { get; set; }
        public string? PerionSorting { get; set; }

        public string? TypeContent { get; set; }
        public int[]? CategoriesIds { get; set; }
    }
}
