namespace CSharpNet_Web_System.Data
{
    using CSharpNet_Web_System.Models.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class CSharpNetWebDbContext : IdentityDbContext<User>
    {
        // TODO According to the official doccumentation for such warnings - CS8618 - we have a different options here -
        // 1. Having default non-empty values for the class members.
        // 2. Initialize them in the constructor.
        // 3. Marking the members as nullable like this - public DbSet<Course>? Courses { get; set; }.

        // Check this link for reference - https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/compiler-messages/nullable-warnings?f1url=%3FappId%3Droslyn%26k%3Dk(CS8618)
        // Site note: Should be refactored in other classes as well.
        public CSharpNetWebDbContext(DbContextOptions<CSharpNetWebDbContext> options)
           : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Tutorial> Tutorials { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<ResourceType> ResourceTypes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Issue> Issues { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Resource>()
              .HasOne(p => p.Tutorial)
              .WithMany(b => b.Resources)
              .OnDelete(DeleteBehavior.Cascade);
        }

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
