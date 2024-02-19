using BookStore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=BookstoreDb;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>()
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Book>()
                .HasOne(b => b.Genre)
                .WithMany(g => g.Books)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Genre>().HasData(
                new Genre() { Id = 1, Name = "Horror" },
                new Genre() { Id = 2, Name = "Thriller" },
                new Genre() { Id = 3, Name = "Romance" },
                new Genre() { Id = 4, Name = "Science Fiction" },
                new Genre() { Id = 5, Name = "Mystery" },
                new Genre() { Id = 6, Name = "Fantasy" },
                new Genre() { Id = 7, Name = "Action" },
                new Genre() { Id = 8, Name = "Adventure" },
                new Genre() { Id = 9, Name = "Historical Fiction" },
                new Genre() { Id = 10, Name = "Humor" }
                );
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
    }
}