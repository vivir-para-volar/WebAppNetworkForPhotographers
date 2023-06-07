using ServerAppNetworkForPhotographers.Models.Data.Dtos.Photographers;

namespace ServerAppNetworkForPhotographers.Models.Data.Dtos.Complaints
{
    public class GetComplaintWithContentDto
    {
        public int Id { get; set; }
        public string? Text { get; set; }
        public string Status { get; set; }

        public ComplaintBase ComplaintBase { get; set; }
        public GetPhotographerForListDto Photographer { get; set; }
        public Content content { get; set; }
    }
}
