using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tennisclub.Models
{
    public class CourtReservation
    {
        [Key]
        public int ReservationId { get; set; }

        [Required]
        public int UserId { get; set; } // Foreign key for User

        [ForeignKey("UserId")]
        public User User { get; set; }

        [Required]
        public int CourtId { get; set; } // Foreign key for Court

        [ForeignKey("CourtId")]
        public Court Court { get; set; }

        [Required]
        public DateTime ReservationDate { get; set; } // Date of the reservation

        [Required]
        [DataType(DataType.Time)]
        public TimeSpan StartTime { get; set; } // Start time of the reservation

        [Required]
        [DataType(DataType.Time)]
        public TimeSpan EndTime { get; set; } // End time of the reservation

        [NotMapped] // Ensures this validation is checked at runtime and not stored in the database
        public bool IsValidTimeRange => StartTime >= TimeSpan.FromHours(7) && EndTime <= TimeSpan.FromHours(22) && StartTime < EndTime;

        [MaxLength(500)]
        public string Notes { get; set; } // Optional notes for the reservation
    }
}
