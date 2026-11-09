<<<<<<< Updated upstream
﻿using System.ComponentModel.DataAnnotations;

namespace DaisyDashRaceDay.API.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string Username { get; set; } = "";

        [Required]
        public string Email { get; set; } = "";

        [Required]
        public string PasswordHash { get; set; } = "";

        public string Role { get; set; } = "User";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
=======
﻿using System.ComponentModel.DataAnnotations;

namespace DaisyDashRaceDay.API.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required]
        public string Username { get; set; } = "";

        [Required]
        public string Email { get; set; } = "";

        [Required]
        public string PasswordHash { get; set; } = "";

        // Must be exactly "Organiser" or "Participant" - Part 3 uses these 2 roles only
        [Required]
        public string Role { get; set; } = "Participant";

        // Part 3: Participant Profile Picture - Blob URL saved here
        public string? ProfilePictureUrl { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
>>>>>>> Stashed changes
}