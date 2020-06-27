using System.ComponentModel.DataAnnotations;

namespace BackendCore.Common.DTO.Login
{
    public class LoginParameters
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}