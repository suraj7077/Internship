using API.Entities;
using Microsoft.EntityFrameworkCore;
using Section6.dattingapp.API.Entities;

namespace API.data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<UserLike> Likes {get;set;}

        protected  override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<UserLike>()
                .HasKey(K => new{K.SourceUserId,K.LikedUserID});
            
            builder.Entity<UserLike>()
                .HasOne(s => s.SourceUser)
                .WithMany(l =>l.LikedUsers)
                .HasForeignKey(s =>s.SourceUserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<UserLike>()
                .HasOne(s => s.LikedUser)
                .WithMany(l =>l.LikedByUsers)
                .HasForeignKey(s =>s.LikedUserID)
                .OnDelete(DeleteBehavior.Cascade);
        }

 
    }
}