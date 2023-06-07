namespace ServerAppNetworkForPhotographers.Models.Data.Dtos.Complaints
{
    public class GetPhotographerCountComplaints
    {
        public int PhotographerId { get; set; }
        public string PhotographerUsername { get; set; }
        public int CountComplaints { get; set; }

        public GetPhotographerCountComplaints(int photographerId, string photographerUsername, int countComplaints)
        {
            PhotographerId = photographerId;
            PhotographerUsername = photographerUsername;
            CountComplaints = countComplaints;
        }
    }
}
