using Microsoft.EntityFrameworkCore;
using PopularMuseumsAPI.Models;

namespace PopularMuseumsAPI.Data {
    public class DataContext : DbContext {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=PopularMuseumsAPI;Trusted_Connection=true;TrustServerCertificate=true;");
        }

        public DbSet<Museum> Museums { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<News> News { get; set; }
    }
}
