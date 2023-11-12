using Microsoft.EntityFrameworkCore;
using Student.Web.Api.Models;

namespace Student.Web.Api.Data
{
    public class StudentDataContext : DbContext
    {
        protected readonly IConfiguration _config;

        public DbSet<Pupil> Students { get; set; }

        public StudentDataContext(
            DbContextOptions<StudentDataContext> options,
            IConfiguration configuration) : base(options)
        {
            _config = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_config.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pupil>(p =>
            {
                p.ToTable("Pupils");
                p.HasKey(x => x.StudentId);
            });
        }
    }
}
