using bendita_ajuda_back_end.Models;

namespace bendita_ajuda_back_end.Repositories.Interfaces
{
	public interface ICategoriaRepository
	{
		IEnumerable<Categoria> GetCategorias();
		IEnumerable<Categoria> GetCategoriasServicos();
		Categoria GetCategoria(int id);
		Categoria GetCategoriaServico(int id);
		Categoria Create(Categoria categoria);
		Categoria Update(Categoria categoria);
		Categoria Delete(int id);
	}
}
