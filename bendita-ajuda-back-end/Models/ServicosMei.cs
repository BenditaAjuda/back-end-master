using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bendita_ajuda_back_end.Models
{
	public class ServicosMei
	{
		[Key]
		public int Id { get; set; }
        public string Nome { get; set; }
		public List<Prestador> Prestadores { get; set; }
	}
}

