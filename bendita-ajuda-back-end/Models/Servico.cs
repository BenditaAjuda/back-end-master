using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace bendita_ajuda_back_end.Models
{
	[Table("Servicos")]
	public class Servico
	{
		[Key]
        public int ServicoId { get; set; }
		[Required]
		[StringLength(80)]
		public string? Nome { get; set; }
		[Required]
		[StringLength(300)]
		public string? ImagemUrl { get; set; }
		public int CategoriaId {  get; set; }
		[JsonIgnore]
		public Categoria? Categoria { get; set; }
		[JsonIgnore]
		public List<Prestador> Prestadores { get; set; }
	}
}
