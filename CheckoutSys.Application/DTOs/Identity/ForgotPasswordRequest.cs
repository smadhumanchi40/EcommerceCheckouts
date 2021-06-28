using System.ComponentModel.DataAnnotations;

namespace CheckoutSys.Application.DTOs.Identity
{
    public class ForgotPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}