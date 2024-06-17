using bendita_ajuda_back_end.Data;
using bendita_ajuda_back_end.Models;
using bendita_ajuda_back_end.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace bendita_ajuda_back_end.Repositories.Services
{
	public class PrestadorRepository : IPrestadorRepository
	{
		private readonly BenditaAjudaDbContext _context;

		public PrestadorRepository(BenditaAjudaDbContext context)
		{
			_context = context;
		}

		public IEnumerable<Prestador> GetPrestadoresPorServico(int id)
		{
			try
			{
				IEnumerable<Prestador> prestadoresServico = _context.Prestadores.Where(s => s.Servicos.Any(se => se.ServicoId == id)).Include(ser => ser.Servicos).ToList();

				return prestadoresServico;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}

