using bendita_ajuda_back_end.Data;
using bendita_ajuda_back_end.Models;
using bendita_ajuda_back_end.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System;

namespace bendita_ajuda_back_end.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class CategoriaController : ControllerBase
	{
		private readonly ICategoriaRepository _repository;
		private readonly ILogger<CategoriaController> _logger;

		public CategoriaController(ICategoriaRepository repository, ILogger<CategoriaController> logger)
		{
			_repository = repository;
			_logger = logger;
		}

		[HttpGet]
		public ActionResult<IEnumerable<Categoria>> GetCategorias()
		{
			var user = "Usuário teste";
			DateTime localDate = DateTime.Now;
		
			_logger.LogInformation($"============ Get/Categoria/============== {user} tentou {localDate}");
			IEnumerable<Categoria> categorias = _repository.GetCategorias();

			if (categorias is null || !categorias.Any())
			{
				return NotFound("Categorias não encontradas");
			}
			_logger.LogInformation($"============ Get/Categoria/============== {user} consegui {localDate}");
			return Ok(categorias);
		}

		[HttpGet("categoriasServicos")]
		public ActionResult<IEnumerable<Categoria>> GetCategoriasServicos()
		{
			IEnumerable<Categoria> categoriasServicos = _repository.GetCategoriasServicos();

			if (categoriasServicos is null || !categoriasServicos.Any())
			{
				return NotFound("Nada encontrado");
			}

			return Ok(categoriasServicos);
		}

		[HttpGet("categoriaServico/{id:int}")]
		public ActionResult<Categoria> GetCategoriaServico(int id)
		{
			Categoria categoriasServico = _repository.GetCategoriaServico(id);

			if (categoriasServico is null)
			{
				return NotFound("Nada encontrado");
			}

			return Ok(categoriasServico);
		}

		[HttpGet("{id:int}", Name = "ObterCategoria")]
		public ActionResult<Categoria> GetCategoria(int id)
		{

			Categoria categoria = _repository.GetCategoria(id);

			if (categoria is null)
			{
				return NotFound($"Categoria com id 1= {id} não encontrado");
			}

			return Ok(categoria);
		}

		[HttpPost]
		public ActionResult Post(Categoria categoria)
		{
			if (categoria is null)
			{
				return BadRequest("Dados não enviados");
			}

			Categoria categoriaCriada = _repository.Create(categoria);

			return new CreatedAtRouteResult("ObterCategoria", new { id = categoriaCriada.CategoriaId }, categoriaCriada);
		}

		[HttpPut("{id:int}")]
		public ActionResult Put(Categoria categoria, int id)
		{
			if (id != categoria.CategoriaId)
			{
				return BadRequest("Dados inválidos");
			}

			_repository.Update(categoria);

			return Ok(categoria);
		}

		[HttpDelete("{id:int}")]
		public ActionResult Delete(int id)
		{
			Categoria categoria = _repository.GetCategoria(id);

			if (categoria is null)
			{
				return NotFound("Categoria não encontrada");
			}

			Categoria categoriaExcluida = _repository.Delete(id);

			return Ok(categoriaExcluida);
		}
	}
}
