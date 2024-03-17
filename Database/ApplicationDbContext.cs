using Microsoft.EntityFrameworkCore;

namespace FinanceControl.Database;

public class ApplicationDbContext : DbContext {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options) : base (options) { }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region seed
            modelBuilder.Entity<Category>().HasData(
                new Category("Course")
            );
            #endregion
        }
    }

public class Category {
    public Category(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }
    public Guid Id { get; set; }
    public string Name { get; set; }
}
