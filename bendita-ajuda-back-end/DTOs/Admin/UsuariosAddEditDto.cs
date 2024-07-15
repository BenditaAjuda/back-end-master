using System.ComponentModel.DataAnnotations;

namespace bendita_ajuda_back_end.DTOs.Admin
{
	public class UsuariosAddEditDto
	{
		public string Id { get; set; }
		[Required]
		public string UserName { get; set; }
		[Required]
		public string FirstName { get; set; }
		[Required]
		public string LastName { get; set; }
		public string Password { get; set; }
		[Required]
		// eg: "Admin,Player,Manager"
		public string Roles { get; set; }
	}
}
