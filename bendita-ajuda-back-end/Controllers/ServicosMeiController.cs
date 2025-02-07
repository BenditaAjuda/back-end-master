using bendita_ajuda_back_end.Models;
using bendita_ajuda_back_end.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bendita_ajuda_back_end.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class ServicosMeiController : ControllerBase
	{

		private readonly IServicosMeiRepository _repository;

		public ServicosMeiController(IServicosMeiRepository repository)
		{
			_repository = repository;
		}

		[HttpGet]
		public ActionResult<IEnumerable<ServicosMei>> GetServicosMei()
		{
			IEnumerable<ServicosMei> servicosMei = _repository.GetServicosMei();

			if (!servicosMei.Any())
			{
				return NotFound("Servicos não encontrados");
			}
			
			return Ok(servicosMei);
		}


	}
}
