using System.ComponentModel.DataAnnotations;

namespace UserClientAppNetworkForPhotographers.Models.Data.Dtos.PhotosInfo
{
    public class CreatePhotoInfoDto
    {
        [Range(1, int.MaxValue)]
        public int PhotoId { get; set; }
    }
}
