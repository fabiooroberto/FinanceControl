namespace FinanceControl.Database;

public class ApplicationDbContext : DbContext {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options) : base (options) { }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region seed
            modelBuilder.Entity<Category>().HasData(
                new Category() { Id = Guid.NewId(), Name = "Course"}
            )
            #endregion
        }
    }

public class Category {
    public Guid Id { get; }
    public string Name { get; set; }
}
