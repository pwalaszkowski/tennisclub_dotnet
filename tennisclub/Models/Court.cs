using System.ComponentModel.DataAnnotations;

namespace tennisclub.Models
{
    public class Court
    {
        [Key]
        public int CourtId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(200)]
        public string Location { get; set; }

        [Required]
        public SurfaceType Surface { get; set; }
    }

    public enum SurfaceType
    {
        Clay,
        Grass,
        Hard,
        Carpet
    }
}