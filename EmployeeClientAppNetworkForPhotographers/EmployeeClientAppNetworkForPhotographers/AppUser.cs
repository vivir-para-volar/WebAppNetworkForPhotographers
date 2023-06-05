namespace EmployeeClientAppNetworkForPhotographers
{
    public static class AppUser
    {
        public static string GetToken(HttpContext httpContext)
        {
            return httpContext.User.Claims.FirstOrDefault(item => item.Type == "Token")?.Value;
        }

        public static int GetUserId(HttpContext httpContext)
        {
            string photographerId = httpContext.User.Claims.FirstOrDefault(item => item.Type == "UserId")?.Value;
            return Convert.ToInt32(photographerId);
        }
    }
}
