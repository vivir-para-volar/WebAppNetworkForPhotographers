namespace ServerAppNetworkForPhotographers.Models.Lists
{
    public static class UserRoles
    {
        public const string Admin = "Admin";
        public const string Employee = "Employee";
        public const string User = "User";

        public const string AdminEmployee = $"{Admin}, {Employee}";
    }
}
