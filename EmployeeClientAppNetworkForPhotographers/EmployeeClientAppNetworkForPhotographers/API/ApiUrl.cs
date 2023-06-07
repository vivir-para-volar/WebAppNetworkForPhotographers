namespace EmployeeClientAppNetworkForPhotographers.API
{
    public static class ApiUrl
    {
        private const string Server = "https://localhost:7180/api";


        public const string Account = $"{Server}/identity";
        public const string Login = $"{Account}/login";
        public const string RegisterEmployee = $"{Account}/register/employee";
        public const string RegisterAdmin = $"{Account}/register/admin";
        public const string UpdatePassword = $"{Account}/password";


        public const string Photographers = $"{Server}/photographers";
        public const string PhotographersPhoto = $"{Photographers}/photo";


        public const string Contents = $"{Server}/contents";
        public const string ContentsBlogs = $"{Contents}/blogs";
        public const string ContentsBlogsMainPhoto = $"{ContentsBlogs}/mainPhoto";


        public const string Photos = $"{Server}/photos";
        //public const string PhotosInfo = $"{Server}/photosInfo";



        public const string ForEmployees = $"{Server}/forEmployees";
        public const string ForEmployeesContent = $"{ForEmployees}/content";
        public const string ForEmployeesPhotographer = $"{ForEmployees}/photographer";
        public const string ForEmployeesContentStatus = $"{ForEmployeesContent}/status";
        public const string ForEmployeesPhotographerStatus = $"{ForEmployeesPhotographer}/status";



        public const string CategoryDirs = $"{Server}/categoryDirs";
        public const string CategoryDirsCheckCategories = $"{CategoryDirs}/checkCategories";
        public const string CategoryDirsWithCategories = $"{CategoryDirs}/withCategories";

        public const string Categories = $"{Server}/categories";
        public const string CategoriesCheckContents = $"{Categories}/checkContents";


        public const string ComplaintsBase = $"{Server}/complaintsBase";
        public const string ComplaintsBaseCheckComplaints = $"{ComplaintsBase}/checkComplaints";

        public const string Complaints = $"{Server}/complaints";
        public const string ComplaintsStatus = $"{Complaints}/status";
        public const string ComplaintsStatusContent = $"{ComplaintsStatus}/content";
        public const string ComplaintsPhotographers = $"{Complaints}/photographers";
        public const string ComplaintsContents = $"{Complaints}/contents";
    }
}