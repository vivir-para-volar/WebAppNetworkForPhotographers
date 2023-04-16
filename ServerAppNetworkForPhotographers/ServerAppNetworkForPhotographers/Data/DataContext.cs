using Microsoft.EntityFrameworkCore;
using ServerAppNetworkForPhotographers.Models;

namespace ServerAppNetworkForPhotographers.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryDir> CategoryDirs { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<ComplaintBase> ComplaintsBase { get; set; }
        public DbSet<Complaint> Complaints { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Photographer> Photographers { get; set; }
        public DbSet<PhotographerInfo> PhotographersInfo { get; set; }
        public DbSet<PhotoInfo> PhotosInfo { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }

    }
}
