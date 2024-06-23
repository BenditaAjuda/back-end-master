using System.ComponentModel.DataAnnotations;

namespace bendita_ajuda_back_end.DTOs.Conta
{
    public class ConfirmEmailDto
    {
        [Required]
        public string Token { get; set; }
        [Required]
        [RegularExpression("^\\w+@[a-zA-Z_]+?\\.[a-zA-Z]{2,3}$", ErrorMessage = "Email invalido")]
        public string Email { get; set; }
    }
}
