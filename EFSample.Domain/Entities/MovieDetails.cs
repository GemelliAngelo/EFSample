using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFSample.Domain.Entities
{
    public class MovieDetails
    {
        [Key]
        [ForeignKey(nameof(MovieId))]
        public int MovieId { get; set; }
        public Movie? Movie { get; set; }

        public DateTime ReleaseDate { get; set; }
        public short Length { get; set; }
        public short Rating { get; set; }
    }
}

