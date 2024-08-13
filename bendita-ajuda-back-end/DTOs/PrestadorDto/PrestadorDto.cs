using bendita_ajuda_back_end.DTOs.ServicoDto;

namespace bendita_ajuda_back_end.DTOs.PrestadorDto
{
	public class PrestadorDto
	{
		public string Logradouro { get; set; }
		public string Bairro { get; set; }
		public string Cidade { get; set; }
		public string Estado { get; set; }
		public string Nome { get; set; }
		public string Email { get; set; }
		public string TelefoneCelular { get; set; }
		public string TelefoneFixo { get; set; }
		public string Complemento { get; set; }
		public string Cep { get; set; }

		public List<ServicosDto> ServicosDto { get; set; }
	}
}
