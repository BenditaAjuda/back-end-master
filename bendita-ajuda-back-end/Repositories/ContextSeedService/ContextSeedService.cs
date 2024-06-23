using bendita_ajuda_back_end.Data;
using bendita_ajuda_back_end.Models.User;
using bendita_ajuda_back_end.Statics;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace bendita_ajuda_back_end.Repositories.ContextSeedService
{
	public class ContextSeedService
	{
		private readonly BenditaAjudaDbContext _context;
		private readonly UserManager<User> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public ContextSeedService(BenditaAjudaDbContext context,
								  UserManager<User> userManager,
								  RoleManager<IdentityRole> roleManager)
		{
			_context = context;
			_userManager = userManager;
			_roleManager = roleManager;
		}

		public async Task InitializeContextAsync()
		{
			if (_context.Database.GetPendingMigrationsAsync().GetAwaiter().GetResult().Count() > 0)
			{
				// applies any pending migration into our database
				await _context.Database.MigrateAsync();
			}

			if (!_roleManager.Roles.Any())
			{
				await _roleManager.CreateAsync(new IdentityRole { Name = SD.AdminRole });
				await _roleManager.CreateAsync(new IdentityRole { Name = SD.ManagerRole });
				await _roleManager.CreateAsync(new IdentityRole { Name = SD.UsuarioRole });
			}

			if (!_userManager.Users.AnyAsync().GetAwaiter().GetResult())
			{
				var admin = new User
				{
					FirstName = "Super",
					LastName = "Admin",
					UserName = "benditaajuda6@gmail.com",
					Email = "benditaajuda6@gmail.com",
					EmailConfirmed = true
				};
				await _userManager.CreateAsync(admin, "123456");
				await _userManager.AddToRolesAsync(admin, new[] { SD.AdminRole, SD.ManagerRole, SD.UsuarioRole });
				await _userManager.AddClaimsAsync(admin, new Claim[]
				{
					new Claim(ClaimTypes.Email, admin.Email),
					new Claim(ClaimTypes.Surname, admin.LastName)
				});

				var manager = new User
				{
					FirstName = "Gerente",
					LastName = "Geral",
					UserName = "estudotsi@gmail.com",
					Email = "estudotsi@gmail.com",
					EmailConfirmed = true
				};
				await _userManager.CreateAsync(manager, "123456");
				await _userManager.AddToRoleAsync(manager, SD.ManagerRole);
				await _userManager.AddClaimsAsync(manager, new Claim[]
				{
					new Claim(ClaimTypes.Email, manager.Email),
					new Claim(ClaimTypes.Surname, manager.LastName)
				});

				var usuario = new User
				{
					FirstName = "Usuario",
					LastName = "Geral",
					UserName = "vidanovaimagens@gmail.com",
					Email = "vidanovaimagens@gmail.com",
					EmailConfirmed = true
				};
				await _userManager.CreateAsync(usuario, "123456");
				await _userManager.AddToRoleAsync(usuario, SD.UsuarioRole);
				await _userManager.AddClaimsAsync(usuario, new Claim[]
				{
					new Claim(ClaimTypes.Email, usuario.Email),
					new Claim(ClaimTypes.Surname, usuario.LastName)
				});

				var vipplayer = new User
				{
					FirstName = "vipplayer",
					LastName = "vip",
					UserName = "rogerio.campos@cg.df.gov.br",
					Email = "rogerio.campos@cg.df.gov.br",
					EmailConfirmed = true
				};
				await _userManager.CreateAsync(vipplayer, "123456");
				await _userManager.AddToRoleAsync(vipplayer, SD.UsuarioRole);
				await _userManager.AddClaimsAsync(vipplayer, new Claim[]
				{
					new Claim(ClaimTypes.Email, vipplayer.Email),
					new Claim(ClaimTypes.Surname, vipplayer.LastName)
				});
			}
		}
	}
}

