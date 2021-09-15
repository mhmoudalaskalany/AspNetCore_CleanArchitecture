using System.ComponentModel.DataAnnotations;

namespace BackendCore.Common.DTO.Identity.Login
{
    public class LoginParameters
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}