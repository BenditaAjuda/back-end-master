using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace bendita_ajuda_back_end.Models
{
	[Table("Categorias")]
	public class Categoria
	{
		public Categoria()
		{
			Servicos = new Collection<Servico>();
		}
		[Key]
        public int CategoriaId { get; set; }
		[Required]
		[StringLength(80)]
		public string? Nome { get; set; }
		public string? Descricao { get; set; }
		[Required]
		[StringLength(300)]
		public string? ImagemUrl { get; set; }
		public ICollection<Servico>? Servicos { get; set; }
	}
}
