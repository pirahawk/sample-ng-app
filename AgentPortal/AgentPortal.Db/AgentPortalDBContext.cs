using AgentPortal.Domain.Data;
using Microsoft.EntityFrameworkCore;

namespace AgentPortal.Db
{
    public class AgentPortalDBContext : DbContext
    {
        public DbSet<Listing> Listings { get; set; }
        public DbSet<ListingImage> Images { get; set; }

        public AgentPortalDBContext(DbContextOptions<AgentPortalDBContext> options)
        :base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
            //{
            //    optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EFProviders.InMemory;Trusted_Connection=True;ConnectRetryCount=0");
            //}
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Listing>().HasKey(l => l.Id);
            modelBuilder.Entity<Listing>()
                .HasMany<ListingImage>(l => l.Images)
                .WithOne(li => li.Listing);

            modelBuilder.Entity<ListingImage>().HasKey(l => l.Id);
        }
    }
}