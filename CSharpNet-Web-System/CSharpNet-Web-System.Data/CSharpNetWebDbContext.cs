namespace CSharpNet_Web_System.Data
{
    using CSharpNet_Web_System.Models.Models;  
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class CSharpNetWebDbContext : IdentityDbContext<User>
    {
        public CSharpNetWebDbContext(DbContextOptions<CSharpNetWebDbContext> options)
           : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Tutorial> Tutorials { get; set; }
        public DbSet<TutorialCategory> TutorialCategories { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<ResourceType> ResourceTypes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostCategory> PostCategories { get; set; }
        public DbSet<Issue> Issues { get; set; }


        public override int SaveChanges()
        {
            ApplyTimestamps();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ApplyTimestamps();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void ApplyTimestamps()
        {
            var entities = ChangeTracker
                .Entries()
                .Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                var now = DateTime.UtcNow;

                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity)entity.Entity).CreatedOn = now;
                }
                ((BaseEntity)entity.Entity).UpdatedOn = now;
            }
        }

    }
}   
