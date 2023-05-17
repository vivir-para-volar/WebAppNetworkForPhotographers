using System.ComponentModel.DataAnnotations;

namespace UserClientAppNetworkForPhotographers.Models.Data.Dtos
{
    public class SearchDto
    {
        [MinLength(2)]
        public string? SearchData { get; set; }
    }
}
