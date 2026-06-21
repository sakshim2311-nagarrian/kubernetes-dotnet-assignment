using Microsoft.EntityFrameworkCore;

namespace ServiceApiTier.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Record> Records { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed 10 initial records
            modelBuilder.Entity<Record>().HasData(
                new Record { Id = 1, Name = "Import Declaration", Description = "Customs import declaration for electronics", CreatedAt = DateTime.UtcNow.AddDays(-10) },
                new Record { Id = 2, Name = "Export Declaration", Description = "Customs export declaration for machinery", CreatedAt = DateTime.UtcNow.AddDays(-9) },
                new Record { Id = 3, Name = "Transit Document", Description = "Transit documentation for goods in transit", CreatedAt = DateTime.UtcNow.AddDays(-8) },
                new Record { Id = 4, Name = "Temporary Storage", Description = "Temporary storage authorization", CreatedAt = DateTime.UtcNow.AddDays(-7) },
                new Record { Id = 5, Name = "Customs Clearance", Description = "Clearance certificate for imported goods", CreatedAt = DateTime.UtcNow.AddDays(-6) },
                new Record { Id = 6, Name = "Invoice Validation", Description = "Commercial invoice validation record", CreatedAt = DateTime.UtcNow.AddDays(-5) },
                new Record { Id = 7, Name = "Certificate of Origin", Description = "Certificate of origin documentation", CreatedAt = DateTime.UtcNow.AddDays(-4) },
                new Record { Id = 8, Name = "Duty Payment", Description = "Customs duty payment confirmation", CreatedAt = DateTime.UtcNow.AddDays(-3) },
                new Record { Id = 9, Name = "Inspection Report", Description = "Goods inspection report", CreatedAt = DateTime.UtcNow.AddDays(-2) },
                new Record { Id = 10, Name = "Release Order", Description = "Final release order for goods", CreatedAt = DateTime.UtcNow.AddDays(-1) }
            );
        }
    }
}