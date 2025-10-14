using EFSample.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFSample.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public DbSet<Movie> Movies { get; init; }
        public DbSet<MovieDetails> MovieDetails { get; init; }
        public DbSet<MovieType> MovieTypes { get; init; }
        public DbSet<Genre> Genres { get; init; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
                builder.UseSqlServer("Server=localhost;Database=EFMoviesDb;User Id=sa;Password=bitspa.1;TrustServerCertificate=true");

            base.OnConfiguring(builder);
        }
    }
}
