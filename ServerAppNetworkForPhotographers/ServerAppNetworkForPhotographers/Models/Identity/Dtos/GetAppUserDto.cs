﻿namespace ServerAppNetworkForPhotographers.Models.Identity.Dtos
{
    public class GetAppUserDto
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

        public GetAppUserDto(string id, string username, string email, string role)
        {
            Id = id;
            Username = username;
            Email = email;
            Role = role;
        }
    }
}
