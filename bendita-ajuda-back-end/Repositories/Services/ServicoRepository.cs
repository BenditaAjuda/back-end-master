using bendita_ajuda_back_end.Data;
using bendita_ajuda_back_end.Models;
using bendita_ajuda_back_end.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace bendita_ajuda_back_end.Repositories.Services
{
	public class ServicoRepository : IServicoRepository
	{

		private readonly BenditaAjudaDbContext _context;

		public ServicoRepository(BenditaAjudaDbContext context)
		{
			_context = context;
		}

		//public IEnumerable<Servico> GetPrestadoresServico(int id)
		//{
		//	try
		//	{
		//		var prestadoresServico = _context.Prestadores.Where(s => s.Servicos.Any(se => se.ServicoId == id)).ToList();
				
		//		if(prestadoresServico is null)
		//		{
		//			return null;
		//		}

		//		return prestadoresServico;		

		//	catch (Exception ex)
		//	{
		//		throw new Exception(ex.Message);
		//	}
		//}
	}
}
