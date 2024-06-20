using bendita_ajuda_back_end.Data;
using bendita_ajuda_back_end.Models;
using bendita_ajuda_back_end.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System;
using bendita_ajuda_back_end.DTOs.Categoria;
using Microsoft.AspNetCore.Authorization;

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

		//aDescricao[Authorize]
		[HttpGet("categoriasDescricao")]
		public ActionResult<List<CategoriaDescricaoDto>> GetCategoriasDescricao()
		{
			List<CategoriaDescricaoDto> categoriaDescricao = new List<CategoriaDescricaoDto>();

			CategoriaDescricaoDto item1 = new CategoriaDescricaoDto { Nome = "Alimentação", Descricao = "Preparo e fornecimento de refeições equilibradas e deliciosas, adaptadas às necessidades e preferências dos clientes.", Icone = "bi bi-egg-fried" };
			CategoriaDescricaoDto item2 = new CategoriaDescricaoDto { Nome = "Construção", Descricao = "Execução de projetos de obras com qualidade, incluindo planejamento, edificação e reformas.", Icone = "bi bi-cone-striped" };
			CategoriaDescricaoDto item3 = new CategoriaDescricaoDto { Nome = "Educação", Descricao = "Ensino e desenvolvimento de habilidades, promovendo aprendizado e crescimento contínuos.", Icone = "bi bi-backpack" };
			CategoriaDescricaoDto item4 = new CategoriaDescricaoDto { Nome = "Saúde", Descricao = "Atendimento e cuidados médicos para promoção e manutenção da saúde.", Icone = "bi bi-bandaid" };
			CategoriaDescricaoDto item5 = new CategoriaDescricaoDto { Nome = "Ar Condicionado", Descricao = "Instalação e manutenção de sistemas de climatização, garantindo conforto térmico e eficiência.", Icone = "bi bi-fan" };
			CategoriaDescricaoDto item6 = new CategoriaDescricaoDto { Nome = "Marcenaria", Descricao = "Criação e reparo de móveis e estruturas em madeira, com design e acabamento personalizado.", Icone = "bi bi-hammer" };
			CategoriaDescricaoDto item7 = new CategoriaDescricaoDto { Nome = "Elétrica", Descricao = "Instalação, reparo e manutenção de sistemas elétricos, garantindo segurança e eficiência energética.", Icone = "bi bi-screwdriver" };
			CategoriaDescricaoDto item8 = new CategoriaDescricaoDto { Nome = "Decoração", Descricao = "lanejamento e execução de ambientes esteticamente agradáveis, adaptados ao estilo e funcionalidade desejados.", Icone = "bi bi-house" };
			CategoriaDescricaoDto item9 = new CategoriaDescricaoDto { Nome = "Informática", Descricao = "Suporte e soluções para sistemas, redes e dispositivos, garantindo eficiência e segurança digital.", Icone = "bi bi-info-circle" };
			CategoriaDescricaoDto item10 = new CategoriaDescricaoDto { Nome = "Babá", Descricao = "Cuidado infantil confiável e personalizado, incluindo supervisão, atividades lúdicas, alimentação, e higiene, proporcionando um ambiente seguro e acolhedor para crianças.", Icone = "bi bi-person-standing-dress" };

			categoriaDescricao.Add(item1);
			categoriaDescricao.Add(item2);
			categoriaDescricao.Add(item3);
			categoriaDescricao.Add(item4);
			categoriaDescricao.Add(item5);
			categoriaDescricao.Add(item6);
			categoriaDescricao.Add(item7);
			categoriaDescricao.Add(item8);
			categoriaDescricao.Add(item9);
			categoriaDescricao.Add(item10);

			return Ok(categoriaDescricao);
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
