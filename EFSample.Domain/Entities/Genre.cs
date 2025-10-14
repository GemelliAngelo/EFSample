using Microsoft.EntityFrameworkCore;

namespace EFSample.Domain.Entities
{
    [PrimaryKey(nameof(Id))]
    public class Genre
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public ICollection<Movie> Movies { get; set; } = [];
    }
}
