using Microsoft.EntityFrameworkCore;

namespace MinimalApi.Data.Context
{
    public class ApplicationDbContext: DbContext
    {
        public DbSet<Student> Students { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("DataSource=MyApp.db;Cache=Shared");
        }
    }
}
