using System.ComponentModel.DataAnnotations;
using SirProject.Core.Enums;

namespace SirProject.Web.DTOs
{
    public class UserCreateDTO
    {
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        public UserRole Role { get; set; } = UserRole.User;
    }

    public class UserUpdateDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Username { get; set; } = string.Empty;
        public string? Password { get; set; } // Optional for update
        [Required]
        public UserRole Role { get; set; } = UserRole.User;
    }

    public class UserResponseDTO
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}

