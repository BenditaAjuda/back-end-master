using bendita_ajuda_back_end.Models;
using bendita_ajuda_back_end.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bendita_ajuda_back_end.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class PrestadorController : ControllerBase
	{
		private readonly IPrestadorRepository _repository;

		public PrestadorController(IPrestadorRepository repository)
		{
			_repository = repository;
		}

		[HttpGet("prestadorServico/{email}")]
		public ActionResult<bool> VerificarExistenciaDePrestador(string email)
		{
			bool prestadorExiste = _repository.ConferirSePrestadorEstaCadastrado(email);

			if (prestadorExiste)
			{
				return BadRequest(true);
			}
			else
			{
				return Ok(false);
			}
		}

		//[HttpGet("prestadorServico/{id:int}")]
		//public ActionResult<IEnumerable<Categoria>> GetPrestadoresPorId(int id)
		//{
		//	IEnumerable<Prestador> prestadores = _repository.GetPrestadoresPorServico(id);

		//	if (prestadores is null || !prestadores.Any())
		//	{
		//		return NotFound("Prestadores não encontrados");
		//	}

		//	return Ok(prestadores);
		//}
	}
}
