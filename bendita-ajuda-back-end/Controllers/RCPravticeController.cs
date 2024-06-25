using bendita_ajuda_back_end.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bendita_ajuda_back_end.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RCPravticeController : ControllerBase
	{
		[HttpGet("public")]
		public IActionResult Public()
		{
			return Ok("public");
		}

		[HttpGet("admin-role")]
		[Authorize(Roles = "Super")]
		public IActionResult AdminRole()
		{
			return Ok("admin role");
		}
	}
}
