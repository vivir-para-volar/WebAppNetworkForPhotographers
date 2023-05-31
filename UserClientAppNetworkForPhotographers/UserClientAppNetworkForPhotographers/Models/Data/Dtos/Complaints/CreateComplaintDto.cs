namespace UserClientAppNetworkForPhotographers.Models.Data.Dtos.Complaints
{
    public class CreateComplaintDto
    {
        public string? Text { get; set; }
        public int ComplaintBaseId { get; set; }
        public int ContentId { get; set; }
    }
}
