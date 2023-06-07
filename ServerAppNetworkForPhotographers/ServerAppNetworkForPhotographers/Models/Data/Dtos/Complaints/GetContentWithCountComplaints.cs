namespace ServerAppNetworkForPhotographers.Models.Data.Dtos.Complaints
{
    public class GetContentWithCountComplaints
    {
        public int ContentId { get; set; }
        public string ContentType { get; set; }

        public int CountComplaints { get; set; }

        public GetContentWithCountComplaints(int contentId, string contentType, int countComplaints)
        {
            ContentId = contentId;
            ContentType = contentType;
            CountComplaints = countComplaints;
        }
    }
}
