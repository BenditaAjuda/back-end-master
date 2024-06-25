using bendita_ajuda_back_end.DTOs.Admin;
using bendita_ajuda_back_end.Models.User;
using bendita_ajuda_back_end.Statics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bendita_ajuda_back_end.Controllers
{
	[Authorize(Roles = "Super")]
	[Route("api/[controller]")]
	[ApiController]
	public class AdminController : ControllerBase
	{
		private readonly UserManager<User> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public AdminController(UserManager<User> userManager,
		  RoleManager<IdentityRole> roleManager)
		{
			_userManager = userManager;
			_roleManager = roleManager;
		}

		[HttpGet("usuarios")]
		public async Task<ActionResult<IEnumerable<UsuariosViewDto>>> GetMembers()
		{
			List<UsuariosViewDto> members = new List<UsuariosViewDto>();
			var users = await _userManager.Users
				.Where(x => x.UserName != "benditaajuda6@gmail.com")
				.ToListAsync();

			foreach (var user in users)
			{
				var memberToAdd = new UsuariosViewDto
				{
					Id = user.Id,
					UserName = user.UserName,
					FirstName = user.FirstName,
					LastName = user.LastName,
					DateCreated = user.DateCreated,
					IsLocked = await _userManager.IsLockedOutAsync(user),
					Roles = await _userManager.GetRolesAsync(user),
				};

				members.Add(memberToAdd);
			}

			return Ok(members);
		}


	}
}
