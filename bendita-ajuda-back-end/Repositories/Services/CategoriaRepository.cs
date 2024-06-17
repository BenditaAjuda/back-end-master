using bendita_ajuda_back_end.Data;
using bendita_ajuda_back_end.Models;
using bendita_ajuda_back_end.Repositories.Interfaces;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;

namespace bendita_ajuda_back_end.Repositories.Services
{
	public class CategoriaRepository : ICategoriaRepository
	{
		private readonly BenditaAjudaDbContext _context;

		public CategoriaRepository(BenditaAjudaDbContext context)
		{
			_context = context;
		}

		public Categoria GetCategoria(int id)
		{
			try
			{
				Categoria categoria = _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);

				return categoria;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
		public Categoria GetCategoriaServico(int id)
		{
			try
			{
				Categoria categoria = _context.Categorias.Include(s => s.Servicos).FirstOrDefault(c => c.CategoriaId == id);
			
				return categoria;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
		public IEnumerable<Categoria> GetCategorias()
		{
			try
			{
				IEnumerable<Categoria> categorias = _context.Categorias.ToList();

				return categorias;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public IEnumerable<Categoria> GetCategoriasServicos()
		{
			try
			{
				IEnumerable<Categoria> categorias = _context.Categorias.Include(s => s.Servicos).AsNoTracking().ToList();

				return categorias;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
		public Categoria Create(Categoria categoria)
		{
			if (categoria is null)
				throw new ArgumentNullException(nameof(categoria));

			try
			{
				_context.Categorias.Add(categoria);
				_context.SaveChanges();

				return categoria;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public Categoria Update(Categoria categoria)
		{
			if (categoria is null)
				throw new ArgumentNullException(nameof(categoria));

			try
			{
				_context.Categorias.Entry(categoria).State = EntityState.Modified;
				_context.SaveChanges();

				return categoria;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public Categoria Delete(int id)
		{
			try
			{
				Categoria categoria = _context.Categorias.Find(id);
				if (categoria is null)

					throw new ArgumentNullException(nameof(categoria));

				_context.Remove(categoria);
				_context.SaveChanges();

				return categoria;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

	}
}
