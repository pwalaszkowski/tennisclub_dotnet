using System.ComponentModel.DataAnnotations;

namespace tennisclub.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [StringLength(100)]
        public string Address { get; set; }

        [Required]
        [StringLength(50)]
        public string MembershipType { get; set; } // np. Basic, Premium

        [Required]
        public DateTime MembershipStartDate { get; set; }

        public DateTime? MembershipEndDate { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Login { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 8)]
        [DataType(DataType.Password)] // Ensures password is treated securely
        public string Password { get; set; }
    }
}