using System.ComponentModel.DataAnnotations.Schema;

namespace EFSample.Domain.Entities
{
    public class MovieType
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        [Column(TypeName = "ntext")]
        public string? Description { get; set; }
    }
}
