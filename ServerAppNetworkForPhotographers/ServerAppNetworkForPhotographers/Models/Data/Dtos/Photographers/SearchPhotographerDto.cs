using System.ComponentModel.DataAnnotations;

namespace ServerAppNetworkForPhotographers.Models.Data.Dtos.Photographers
{
    public class SearchPhotographerDto
    {
        [StringLength(32, MinimumLength = 2)]
        public string? Name { get; set; }
    }
}
