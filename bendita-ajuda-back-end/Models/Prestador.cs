using System.Text.Json.Serialization;

namespace bendita_ajuda_back_end.Models
{
	public class Prestador
	{
		public int PrestadorId { get; set; }
		public string Nome { get; set; }
        public string Endereco { get; set; }
		public List<Servico> Servicos { get; set; }
    }
}
