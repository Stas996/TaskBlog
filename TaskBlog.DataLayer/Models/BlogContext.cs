using Microsoft.AspNet.Identity.EntityFramework;

namespace TaskBlog.DataLayer
{
    using System.Data.Entity;

    public partial class BlogContext : IdentityDbContext<User>
    {
        public BlogContext()
            : base("BlogContext")
        {
            
        }

        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });

            modelBuilder.Entity<UserProfile>()
            .HasRequired(p => p.User);

            modelBuilder.Entity<Post>().ToTable("Post");

            modelBuilder.Entity<Post>()
            .HasRequired(p => p.User);

            modelBuilder.Entity<Post>()
            .HasMany(a => a.Tags)
            .WithMany(t => t.Articles)
            .Map(mc =>
            {
                mc.ToTable("TagPost");
                mc.MapLeftKey("PostId");
                mc.MapRightKey("TagId");
            });

        }

        public static BlogContext Create()
        {
            return new BlogContext();
        }
    }
}
