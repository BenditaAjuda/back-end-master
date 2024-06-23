using System.ComponentModel.DataAnnotations;

namespace bendita_ajuda_back_end.DTOs.Conta
{
    public class ResetPasswordDto
    {
        [Required]
        public string Token { get; set; }
        [Required]
        [RegularExpression("^\\w+@[a-zA-Z_]+?\\.[a-zA-Z]{2,3}$", ErrorMessage = "Email inválido")]
        public string Email { get; set; }
        [Required]
        [StringLength(15, MinimumLength = 6, ErrorMessage = "Password deve ter 6 caracteres")]
        public string NewPassword { get; set; }
    }
}

