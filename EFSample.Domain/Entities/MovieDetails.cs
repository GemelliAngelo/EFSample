using System.ComponentModel.DataAnnotations;

namespace EFSample.Domain.Entities
{
    public class MovieDetails
    {
        [Key]
        public int MovieId { get; set; }
        public Movie? Movie { get; set; }


    }
}
