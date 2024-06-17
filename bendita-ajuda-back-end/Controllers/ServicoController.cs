using bendita_ajuda_back_end.Data;
using bendita_ajuda_back_end.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bendita_ajuda_back_end.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class ServicoController : ControllerBase
	{
		private readonly BenditaAjudaDbContext _context;

		public ServicoController(BenditaAjudaDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public ActionResult<IEnumerable<Servico>> GetServicos()
		{
			var servicos = _context.Servicos.ToList();

			if (servicos is null)
			{
				return NotFound("Servicos não encontrados");
			}

			return servicos;
		}

		[HttpGet("{id:int}", Name = "ObterServico")]
		public ActionResult<Servico> GetServico(int id)
		{
			var servico = _context.Servicos.FirstOrDefault(s => s.ServicoId == id);

			if (servico is null)
			{
				return NotFound("Servico não encontrado");
			}

			return servico;
		}

		[HttpPost]
		public ActionResult Post(Servico servico)
		{
			if (servico is null)
				return BadRequest("Serviço não enviado");

			_context.Servicos.Add(servico);
			_context.SaveChanges();

			return new CreatedAtRouteResult("ObterServico", new { id = servico.ServicoId }, servico);
		}

		[HttpPut("{id:int}")]
		public ActionResult Put(Servico servico, int id)
		{
			if (id != servico.ServicoId)
			{
				return BadRequest();
			}

			_context.Entry(servico).State = EntityState.Modified;
			_context.SaveChanges();

			return Ok(servico);
		}

		[HttpDelete("{id:int}")]
		public ActionResult Delete(int id)
		{
			var servico = _context.Servicos.FirstOrDefault(s => s.ServicoId == id);

			if (servico is null)
			{
				return NotFound("Serviço não encontrado");
			}

			_context.Servicos.Remove(servico);
			_context.SaveChanges();

			return Ok(servico);
		}
	}
}
