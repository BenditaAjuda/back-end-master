using System.ComponentModel.DataAnnotations;

namespace bendita_ajuda_back_end.DTOs.Conta
{
	public class RegisterDto
	{
		[Required]
		[StringLength(15, MinimumLength = 3, ErrorMessage = "First name must be at least {2}, and maximum {1} characters")]
		public string FirstName { get; set; }
		[Required]
		[StringLength(15, MinimumLength = 3, ErrorMessage = "Last name must be at least {2}, and maximum {1} characters")]
		public string LastName { get; set; }
		[Required]
		[RegularExpression("^\\w+@[a-zA-Z_]+?\\.[a-zA-Z]{2,3}$", ErrorMessage = "Email invalido")]
		public string Email { get; set; }
		[Required]
		[StringLength(6, MinimumLength = 6, ErrorMessage = "Password must be at least {2}, and maximum {1} characters")]
		public string Password { get; set; }
    }
}
