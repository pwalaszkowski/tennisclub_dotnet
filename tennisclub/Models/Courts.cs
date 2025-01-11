using System.ComponentModel.DataAnnotations;

namespace tennisclub.Models
{
    public class Court
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(200)]
        public string Location { get; set; }

        [Required]
        [StringLength(50)]
        public string Surface { get; set; } // Examples: Grass, Clay, Hard
    }
}