using System.ComponentModel.DataAnnotations;

namespace bendita_ajuda_back_end.DTOs.Conta
{
	public class RegisterDto
	{
		[Required]
		[StringLength(15, MinimumLength = 3, ErrorMessage = "Entre {2}, e {1} caracteres")]
		public string FirstName { get; set; }
		[Required]
		[StringLength(15, MinimumLength = 3, ErrorMessage = "Entre {2}, e {1} caracteres")]
		public string LastName { get; set; }
		[Required]
		[RegularExpression("^\\w+@[a-zA-Z_]+?\\.[a-zA-Z]{2,3}$", ErrorMessage = "Email invalido")]
		public string Email { get; set; }
		[Required]
		[StringLength(6, MinimumLength = 6, ErrorMessage = "Password ¨deve ter 6 caracteres")]
		public string Password { get; set; }
    }
}
