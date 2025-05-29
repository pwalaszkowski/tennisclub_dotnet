using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using tennisclub.Models;

public class CourtReservation
{
    [Key]
    public int ReservationId { get; set; }

    [Required]
    public int UserId { get; set; }

    public virtual User? User { get; set; }  // Make nullable

    [Required]
    public int CourtId { get; set; }

    public virtual Court? Court { get; set; }  // Make nullable

    [Required]
    public DateTime ReservationDate { get; set; }

    [Required]
    [DataType(DataType.Time)]
    public TimeSpan StartTime { get; set; }

    [Required]
    [DataType(DataType.Time)]
    public TimeSpan EndTime { get; set; }

    [NotMapped]
    public bool IsValidTimeRange => StartTime >= TimeSpan.FromHours(7) &&
                                  EndTime <= TimeSpan.FromHours(22) &&
                                  StartTime < EndTime;

    [MaxLength(500)]
    public string? Notes { get; set; }  // Make nullable
}