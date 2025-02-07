using bendita_ajuda_back_end.Models;

namespace bendita_ajuda_back_end.Repositories.Interfaces
{
	public interface IServicosMeiRepository
	{
		IEnumerable<ServicosMei> GetServicosMei();
	}
}
