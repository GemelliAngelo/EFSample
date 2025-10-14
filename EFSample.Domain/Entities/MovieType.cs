using System.ComponentModel.DataAnnotations;

namespace EFSample.Domain.Entities
{
    public class MovieType
    {
        [Key]
        public int Id { get; set; }

        public required string Name { get; set; }

        public string? Description { get; set; }
        public ICollection<Movie> Movies { get; set; } = [];
    }
}
