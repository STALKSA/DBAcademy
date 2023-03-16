using Microsoft.EntityFrameworkCore;
using StudentsManager.Entities;
using System.Diagnostics;

namespace StudentsManager
{
    public class AppDbContext : DbContext
    {
        private const string ConnectionString = "Data Source=\"C:\\Users\\Ksy18\\OneDrive\\Рабочий стол\\ADO\\StudentsManager\\StudentsManager\\hello.db\"";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(ConnectionString)
                .LogTo(test =>
                {
                    Debug.WriteLine(test);
                });
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>(e =>
            {
                e.Property(o => o.Name)
                .HasColumnType("TEXT COLLATE NOCASE");
            });
        }

        public DbSet<Student> Students => Set<Student>();
        public DbSet<Visit> Visits => Set<Visit>();
        public DbSet<Subject> Subjects => Set<Subject>();
        public DbSet<Group> Groups => Set<Group>();
    }

}
