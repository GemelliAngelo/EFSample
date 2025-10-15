using EFSample.Domain.Entities;
using EFSample.Infrastructure.Extensions;
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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            #region Column Definitions

            //builder.Entity<Movie>().Property(m => m.Id).UseHiLo();

            //builder.Entity<MovieGenre>().HasKey(x => new { x.MovieId, x.GenreId });

            builder.Entity<MovieDetails>(entity =>
            {
                entity.HasKey(d => d.MovieId);
                entity.HasOne(d => d.Movie).WithOne(m => m.MovieDetails).OnDelete(DeleteBehavior.Cascade);
                entity.Property(d => d.ReleaseDate).HasDefaultValueSql("getdate()");
            });

            #endregion

            #region Data seeding

            builder.SeedData();

            #endregion
        }
    }
}
