using System.Text.Json.Serialization;

namespace bendita_ajuda_back_end.Models
{
	public class Prestador
	{
		public int PrestadorId { get; set; }
		public string Email { get; set; }
		public string TelefoneCelular { get; set; }
		public string TelefoneFixo { get; set; }
		public string Nome { get; set; }
        public string Cep { get; set; }
		public string Logradouro { get; set; }
		public string Bairro { get; set; }
		public string Cidade { get; set; }
		public string Estado { get; set; }
		public string Complemento { get; set; }
		public List<Servico> Servicos { get; set; }
		public List<ServicosMei> ServicosMei { get; set; }
	}
}
