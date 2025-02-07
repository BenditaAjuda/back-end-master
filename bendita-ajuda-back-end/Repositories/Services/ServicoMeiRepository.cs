using bendita_ajuda_back_end.Data;
using bendita_ajuda_back_end.Models;
using bendita_ajuda_back_end.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace bendita_ajuda_back_end.Repositories.Services
{
	public class ServicoMeiRepository : IServicosMeiRepository
	{

		private readonly BenditaAjudaDbContext _context;

		public ServicoMeiRepository(BenditaAjudaDbContext context)
		{
			_context = context;
		}

		public IEnumerable<ServicosMei> GetServicosMei()
		{
			try
			{
				IEnumerable<ServicosMei> servicosMei = _context.ServicosMei.ToList();

				return servicosMei;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}
