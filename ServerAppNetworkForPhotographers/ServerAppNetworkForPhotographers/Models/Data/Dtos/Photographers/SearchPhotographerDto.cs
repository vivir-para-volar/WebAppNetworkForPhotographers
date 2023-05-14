using System.ComponentModel.DataAnnotations;

namespace ServerAppNetworkForPhotographers.Models.Data.Dtos.Photographers
{
    public class SearchPhotographerDto
    {
        [MinLength(2)]
        public string? Name { get; set; }
    }
}
