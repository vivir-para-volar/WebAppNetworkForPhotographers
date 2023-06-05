namespace EmployeeClientAppNetworkForPhotographers
{
    public static class AppUser
    {
        public static string GetToken(HttpContext httpContext)
        {
            return httpContext.User.Claims.FirstOrDefault(item => item.Type == "Token")?.Value;
        }

        public static string GetUserId(HttpContext httpContext)
        {
            return httpContext.User.Claims.FirstOrDefault(item => item.Type == "UserId")?.Value;
        }
    }
}
