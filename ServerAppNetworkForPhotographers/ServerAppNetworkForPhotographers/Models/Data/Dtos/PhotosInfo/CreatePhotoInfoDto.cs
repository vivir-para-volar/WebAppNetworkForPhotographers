using System.ComponentModel.DataAnnotations;

namespace ServerAppNetworkForPhotographers.Models.Data.Dtos.PhotosInfo
{
    public class CreatePhotoInfoDto
    {
        [Range(1, int.MaxValue)]
        public int PhotoId { get; set; }
    }
}
