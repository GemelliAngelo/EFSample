using System.ComponentModel.DataAnnotations.Schema;

namespace EFSample.Domain.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public required string Title { get; set; }

        [Column(TypeName = "ntext")]
        public string? Description { get; set; }
        public MovieDetails? MovieDetails { get; set; }

        [ForeignKey(nameof(MovieType))]
        public int MovieTypeId { get; set; }
        public MovieType? MovieType { get; set; }

        public ICollection<Genre> Genres { get; set; } = [];
    }
}
