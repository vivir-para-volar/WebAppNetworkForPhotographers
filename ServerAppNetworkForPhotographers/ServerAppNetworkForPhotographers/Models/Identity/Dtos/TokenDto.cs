namespace ServerAppNetworkForPhotographers.Models.Identity.Dtos
{
    public class TokenDto
    {
        public string Token { get; set; }
        public string Role { get; set; }
        public int? PhotographerId { get; set; }
    }
}
