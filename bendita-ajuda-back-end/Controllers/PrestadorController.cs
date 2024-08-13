using bendita_ajuda_back_end.Data;
using bendita_ajuda_back_end.DTOs.PrestadorDto;
using bendita_ajuda_back_end.Models;
using bendita_ajuda_back_end.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bendita_ajuda_back_end.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class PrestadorController : ControllerBase
	{
		private readonly IPrestadorRepository _repository;
		private readonly BenditaAjudaDbContext _context;

		public PrestadorController(IPrestadorRepository repository, BenditaAjudaDbContext context)
		{
			_repository = repository;
			_context = context;
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

		[HttpPost]
		public async Task<IActionResult> CreatePrestador([FromBody] PrestadorDto prestadorDto)
		{
			var servicos = await _context.ServicosMei.Where(s => prestadorDto.ServicosDto.Select(serv => serv.Id).Contains(s.Id)).ToListAsync();

			var prestador = new Prestador
			{
				Nome = prestadorDto.Nome,
				Logradouro = prestadorDto.Logradouro,
				Bairro = prestadorDto.Bairro,
				Cidade = prestadorDto.Cidade,
				Estado = prestadorDto.Estado,
				Email = prestadorDto.Email,
				TelefoneCelular = prestadorDto.TelefoneCelular,
				TelefoneFixo = prestadorDto.TelefoneFixo,
				Complemento = prestadorDto.Complemento,
				Cep = prestadorDto.Cep,
				ServicosMei = servicos // Associate the related ServicoMei entities
			};

			_context.Prestadores.Add(prestador);
			await _context.SaveChangesAsync();

			return Ok(prestador);
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
