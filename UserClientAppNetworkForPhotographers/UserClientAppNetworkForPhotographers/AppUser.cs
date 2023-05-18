using UserClientAppNetworkForPhotographers.Models.Data;

namespace UserClientAppNetworkForPhotographers
{
    public static class AppUser
    {
        public static string? Token { get; set; }
        public static Photographer Photographer { get; set; }

        static AppUser()
        {
            Photographer = new Photographer();
        }

        public static void Clear()
        {
            Token = null;
            Photographer = new Photographer();
        }
    }
}
