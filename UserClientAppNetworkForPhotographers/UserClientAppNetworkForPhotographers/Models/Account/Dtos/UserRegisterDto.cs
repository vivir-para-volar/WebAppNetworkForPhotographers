namespace UserClientAppNetworkForPhotographers.Models.Account.Dtos
{
    public class UserRegisterDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public UserRegisterDto(string username, string email, string password)
        {
            Username = username;
            Email = email;
            Password = password;
        }
    }
}
