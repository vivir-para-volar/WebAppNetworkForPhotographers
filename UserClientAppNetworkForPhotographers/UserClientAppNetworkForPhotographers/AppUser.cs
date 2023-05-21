namespace UserClientAppNetworkForPhotographers
{
    public static class AppUser
    {
        public static string GetToken(HttpContext httpContext)
        {
            return httpContext.User.Claims.FirstOrDefault(item => item.Type == "Token")?.Value;
        }

        public static int GetPhotographerId(HttpContext httpContext)
        {
            string photographerId = httpContext.User.Claims.FirstOrDefault(item => item.Type == "PhotographerId")?.Value;
            return Convert.ToInt32(photographerId);
        }
    }
}
