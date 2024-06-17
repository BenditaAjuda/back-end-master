using bendita_ajuda_back_end.DTOs.Conta;
using bendita_ajuda_back_end.Models.User;
using bendita_ajuda_back_end.Repositories.AuthServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace bendita_ajuda_back_end.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class ContaController : ControllerBase
	{
		private readonly JWTService _jwtService;
		private readonly SignInManager<User> _signInManager;
		private readonly UserManager<User> _userManager;

		public ContaController(UserManager<User> userManager,
							   JWTService jwtService,
							   SignInManager<User> signInManager)
		{
			_userManager = userManager;
			_jwtService = jwtService;
			_signInManager = signInManager;
		}

		[HttpPost("login")]
		public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
		{
			var user = await _userManager.FindByNameAsync(loginDto.UserName);

			if (user == null)
				return Unauthorized("Login ou senha incorretas");
			if (user.EmailConfirmed == false)
				return Unauthorized("Confirme seu email");

			var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

			if (!result.Succeeded)
				return Unauthorized("Login ou senha incorretas");

			return CreateApplicationUserDto(user);

		}

		[HttpPost("register")]
		public async Task<IActionResult> Register(RegisterDto registerDto)
		{
			if(await CheckEmailExistsAsync(registerDto.Email))
			{
				return BadRequest($"Existe uma conta usando o email {registerDto.Email} tente outro");
			}

			var userToAdd = new User
			{
				FirstName = registerDto.FirstName.ToLower(),
				LastName = registerDto.LastName.ToLower(),
				UserName = registerDto.Email.ToLower(),
				Email = registerDto.Email.ToLower(),
				EmailConfirmed = true
			};

			var result = await _userManager.CreateAsync(userToAdd, registerDto.Password);

			if (!result.Succeeded)
				return BadRequest(result.Errors);

			return Ok("Conta criada, pode logar");
		}

		[Authorize]
		[HttpGet("refresh-user-token")]
		public async Task<ActionResult<UserDto>> RefreshUserToken()
		{
			var user = await _userManager.FindByNameAsync(User.FindFirst(ClaimTypes.Email)?.Value);
			return CreateApplicationUserDto(user);
		}

		private async Task<bool> CheckEmailExistsAsync(string email)
		{
			return await _userManager.Users.AnyAsync(e => e.Email == email.ToLower());
		}
		
		private UserDto CreateApplicationUserDto(User user)
		{
			return new UserDto
			{
				FirstName = user.FirstName,
				LastName = user.LastName,
				JWT = _jwtService.CreateJWT(user)
			};
		}
	}
}
