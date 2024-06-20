using System.ComponentModel.DataAnnotations;

namespace bendita_ajuda_back_end.DTOs.Conta
{
	public class LoginDto
	{
        [Required (ErrorMessage = "Usuário obrigatório")]
		public string UserName { get; set; }
        [Required (ErrorMessage = "Senha obrigatória")]
		public string Password { get; set; }
    }
}
