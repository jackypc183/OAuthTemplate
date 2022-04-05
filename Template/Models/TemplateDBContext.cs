using Microsoft.EntityFrameworkCore;
using Template.Models.Db;

namespace Template
{
    public class TemplateDBContext : DbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<LineNotifyToken> LineNotifyToken { get; set; }
        public TemplateDBContext(DbContextOptions<TemplateDBContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
