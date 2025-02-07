using bendita_ajuda_back_end.Models;
using bendita_ajuda_back_end.Models.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace bendita_ajuda_back_end.Data
{
	public class BenditaAjudaDbContext : IdentityDbContext<User>
	{
		public BenditaAjudaDbContext(DbContextOptions<BenditaAjudaDbContext> options) : base(options)
		{

		}

		public DbSet<Categoria> Categorias { get; set; }
		public DbSet<Servico> Servicos { get; set; }
		public DbSet<Prestador> Prestadores { get; set; }
		public DbSet<ServicosMei> ServicosMei { get; set; }
	}
}
