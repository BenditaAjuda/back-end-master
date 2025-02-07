using bendita_ajuda_back_end.Data;
using bendita_ajuda_back_end.DTOs.PrestadorDto;
using bendita_ajuda_back_end.DTOs.ServicoDto;
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
				Lat = prestadorDto.Lat,
				Long = prestadorDto.Long,
				Cep = prestadorDto.Cep,
				ServicosMei = servicos // Associate the related ServicoMei entities
			};

			_context.Prestadores.Add(prestador);
			await _context.SaveChangesAsync();

			return Ok(prestador);
		}

		[HttpGet("servicosMeiPrestador/{email}")]
		public ActionResult<List<ServicosMei>> ConsultarServicosPorPrestador(string email)
		{
			var servicosMeiList = _context.Prestadores
			.Where(p => p.Email == email)
			.SelectMany(p => p.ServicosMei)
			.ToList();

			return servicosMeiList;
		}

		[HttpGet("dadosPrestador/{email}")]
		public ActionResult<PrestadorDto> ConsultarDadosPrestador(string email)
		{
			var prestadorDto = _context.Prestadores
				.Where(p => p.Email == email)
				.Select(p => new PrestadorDto
				{
					Logradouro = p.Logradouro,
					Bairro = p.Bairro,
					Cidade = p.Cidade,
					Estado = p.Estado,
					Nome = p.Nome,
					Email = p.Email,
					TelefoneCelular = p.TelefoneCelular,
					TelefoneFixo = p.TelefoneFixo,
					Complemento = p.Complemento,
					Cep = p.Cep,

					// Mapping ServicosMei to ServicosDto
					ServicosDto = p.ServicosMei.Select(s => new ServicosDto
					{
						Nome = s.Nome
					}).ToList()
				})
				.FirstOrDefault();

			if (prestadorDto == null)
			{
				return NotFound();
			}

			return Ok(prestadorDto);
		}

		[HttpGet("prestadoresPorServico/{id}")]
		public ActionResult<List<Prestador>> ConsultarPrestadoresPorServico(int id)
		{
			var prestadores = _context.Prestadores.Where(p => p.ServicosMei.Any(s => s.Id == id)).ToList();
			return Ok(prestadores);
		}

	}
}
