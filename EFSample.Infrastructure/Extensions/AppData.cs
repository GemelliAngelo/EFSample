using EFSample.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFSample.Infrastructure.Extensions
{
    public static class EFExtensions
    {
        public static void SeedData(this ModelBuilder builder)
        {
            Genre[] genres = [
                new(){Id = 1, Name = "Comedy" },
                new(){Id = 2, Name = "Action" },
                new(){Id = 3, Name = "Fantasy" },
                new(){Id = 4, Name = "Adventure" },
            ];

            builder.Entity<Genre>().HasData(genres);

            MovieType[] types = [
                new(){Id = 1, Name = "Film" },
                new(){Id = 2, Name = "TV-show" },
                new(){Id = 3, Name = "Documentary" },
                new(){Id = 4, Name = "Reality" },
            ];

            builder.Entity<MovieType>().HasData(types);
        }
    }
}
