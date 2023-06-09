﻿namespace ServerAppNetworkForPhotographers.Models.Data.Dtos.Photographers
{
    public class GetPhotographerForListDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string? Name { get; set; }
        public string? PhotoProfile { get; set; }

        public GetPhotographerForListDto(int id, string username, string? name, string? photoProfile)
        {
            Id = id;
            Username = username;
            Name = name;
            PhotoProfile = photoProfile;
        }
    }
}
