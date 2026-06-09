using Microsoft.EntityFrameworkCore;
namespace VeloceCRM.Data
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {

        }
        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries().Where(x => x.Entity.GetType().GetProperty("Created") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("Created").CurrentValue = Veloce.Core.ConvertDateTimeToLong(DateTime.Now);
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Property("Created").IsModified = false;
                    entry.Property("Updated").CurrentValue = Veloce.Core.ConvertDateTimeToLong(DateTime.Now);
                }
            }
            return base.SaveChanges();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Entity.Person> Persons { get; set; }
        public DbSet<Entity.Company> Companies { get; set; }
        public DbSet<Entity.Postalzone> Postalzones { get; set; }
        public DbSet<Entity.Location> Locations { get; set; }
        public DbSet<Entity.Country> Countries { get; set; }
        public DbSet<Entity.User> Users { get; set; }
        public DbSet<Entity.License> Licenses { get; set; }
    }
}
