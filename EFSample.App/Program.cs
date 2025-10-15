using EFSample.Domain.Entities;
using EFSample.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EFSample.App
{
    internal class Program
    {
        private static readonly JsonSerializerOptions _options = new() { WriteIndented = true, ReferenceHandler = ReferenceHandler.IgnoreCycles };

        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Console()
                .CreateLogger();

            try
            {
                Log.Information($"App info: {Assembly.GetExecutingAssembly().FullName}");
                using var context = new AppDbContext();
                var types = context.MovieTypes.ToList();
                var genres = context.Genres.ToList();

                while (true)
                {
                    Console.WriteLine("Please choose an option:");
                    Console.WriteLine("1. To print movie list");
                    Console.WriteLine("2. To insert a new movie");
                    Console.WriteLine("3. To exit");

                    var option = Convert.ToInt16(Console.ReadLine());

                    switch (option)
                    {
                        case 1:
                            var movies = context.Movies
                                .Include(m => m.MovieDetails)
                                .Include(m => m.MovieType)
                                .Include(m => m.Genres)
                                .ToList();

                            Console.WriteLine(JsonSerializer.Serialize(movies, _options));

                            //var movie = movies.FirstOrDefault(m => m.Id == 1);
                            //var genres = movie?.Genres.Select(g => g.Name).ToList();

                            break;
                        case 2:
                            Console.WriteLine("Movie title:");
                            var title = Console.ReadLine()!;

                            Console.WriteLine("Movie description:");
                            var description = Console.ReadLine();

                            Console.WriteLine("Release date:");
                            var releaseDate = Convert.ToDateTime(Console.ReadLine());

                            Console.WriteLine("Rating from 1 to 10:");
                            var rating = Convert.ToInt16(Console.ReadLine());

                            Console.WriteLine("Movie length:");
                            var length = Convert.ToInt16(Console.ReadLine());

                            Console.WriteLine("Available types:");
                            foreach (var item in types)
                            {
                                Console.WriteLine($"[{item.Id}] - {item.Name}");
                            }
                            var type = Convert.ToInt16(Console.ReadLine());

                            Console.WriteLine("How many genres: ");
                            var genresCount = Convert.ToInt16(Console.ReadLine());
                            ICollection<int> genreIds = [];
                            ICollection<Genre> genresList = [];

                            Console.WriteLine("Available genres:");
                            foreach (var item in genres)
                            {
                                Console.WriteLine($"[{item.Id}] - {item.Name}");
                            }

                            for (int i = 0; i < genresCount; i++)
                            {
                                Console.Write("Genre Id: ");
                                genreIds.Add(Convert.ToInt32(Console.ReadLine()));
                            }

                            genresList = [.. context.Genres.Where(g => genreIds.Contains(g.Id))];

                            Console.WriteLine(JsonSerializer.Serialize(genresList));

                            var movie = new Movie()
                            {
                                Title = title,
                                Description = description,
                                MovieDetails = new() { ReleaseDate = releaseDate, Length = length, Rating = rating },
                                MovieTypeId = type,
                                Genres = genresList
                            };

                            context.Movies.Add(movie);
                            context.SaveChanges();
                            break;
                        case 3:
                            return;
                    }
                }

                //
                //int[] selectedGenreIds = [1, 2];
                //var query = context.Genres.Where(g => selectedGenreIds.Contains(g.Id));
                //Log.Information(query.ToQueryString());
                //var genres = query.ToList();
                //var movie = new Movie()
                //{
                //    Title = "TLOTR - second movie!",
                //    Description = "desc...",
                //    MovieDetails = new() { ReleaseDate = new DateTime(2025, 1, 1), Length = 180, Rating = 10 },
                //    MovieTypeId = 1,
                //    Genres = genres
                //};

                //var result = context.Movies.Add(movie).Entity;
                ////context.MovieDetails.Add(new() { MovieId = result.Id, ReleaseDate = new DateTime(2025, 1, 1), Length = 180, Rating = 10 });
                //context.SaveChanges();

                //Console.WriteLine(JsonSerializer.Serialize(result, _options));
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
