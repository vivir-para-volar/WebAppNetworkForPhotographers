﻿namespace EmployeeClientAppNetworkForPhotographers.API
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
        //public const string PhotographersSearch = $"{Photographers}/search";

        //public const string PhotographersInfo = $"{Server}/photographersInfo";

        //public const string Subscriptions = $"{Server}/subscriptions";
        //public const string SubscriptionsCheck = $"{Subscriptions}/check";
        //public const string SubscriptionsCountSubscribers = $"{Subscriptions}/countSubscribers";
        //public const string SubscriptionsCountSubscriptions = $"{Subscriptions}/countSubscriptions";
        //public const string SubscriptionsSubscribers = $"{Subscriptions}/subscribers";
        //public const string SubscriptionsSubscriptions = $"{Subscriptions}/subscriptions";


        public const string Contents = $"{Server}/contents";
        //public const string ContentsPosts = $"{Contents}/posts";
        public const string ContentsBlogs = $"{Contents}/blogs";
        public const string ContentsBlogsMainPhoto = $"{ContentsBlogs}/mainPhoto";

        //public const string ContentsPostsUser = $"{ContentsPosts}/user";
        //public const string ContentsBlogsUser = $"{ContentsBlogs}/user";

        //public const string ContentsPostsPhotographer = $"{ContentsPosts}/photographer";
        //public const string ContentsBlogsPhotographer = $"{ContentsBlogs}/photographer";

        //public const string ContentsPostsFavourites = $"{ContentsPosts}/favourites";
        //public const string ContentsBlogsFavourites = $"{ContentsBlogs}/favourites";

        //public const string ContentsPostsSearch = $"{ContentsPosts}/search";
        //public const string ContentsBlogsSearch = $"{ContentsBlogs}/search";

        //public const string ContentsNews = $"{Contents}/news";
        //public const string ContentsOthers = $"{Contents}/others";


        public const string Photos = $"{Server}/photos";
        //public const string PhotosInfo = $"{Server}/photosInfo";


        public const string CategoryDirs = $"{Server}/categoryDirs";
        public const string CategoryDirsCheckCategories = $"{CategoryDirs}/checkCategories";
        public const string CategoryDirsWithCategories = $"{CategoryDirs}/withCategories";

        public const string Categories = $"{Server}/categories";
        public const string CategoriesCheckContents = $"{Categories}/checkContents";


        //public const string Likes = $"{Server}/likes";
        //public const string LikesContent = $"{Likes}/content";

        //public const string Favourites = $"{Server}/favourites";

        //public const string Comments = $"{Server}/comments";
        //public const string CommentsContent = $"{Comments}/content";

        public const string ComplaintsBase = $"{Server}/complaintsBase";
        public const string ComplaintsBaseCheckComplaints = $"{ComplaintsBase}/checkComplaints";
        
        //public const string Complaints = $"{Server}/complaints";
    }
}