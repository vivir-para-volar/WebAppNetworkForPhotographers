namespace UserClientAppNetworkForPhotographers.Models.Data.Dtos.Photographers
{
    public class UpdatePhotographerDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string? Name { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }

        public UpdatePhotographerDto(GetPhotographerWithInfoDto photographer)
        {
            Id = photographer.Id;
            Username = photographer.Username;
            Email = photographer.Email;
            Name = photographer.Name;
            Country = photographer.Country;
            City = photographer.City;
        }
    }
}
