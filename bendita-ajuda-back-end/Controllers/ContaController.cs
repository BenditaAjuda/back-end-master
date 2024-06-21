using bendita_ajuda_back_end.DTOs.Conta;
using bendita_ajuda_back_end.DTOs.EmailSend;
using bendita_ajuda_back_end.Models.User;
using bendita_ajuda_back_end.Repositories.AuthServices;
using bendita_ajuda_back_end.Repositories.EmailService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text;

namespace bendita_ajuda_back_end.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class ContaController : ControllerBase
	{
		private readonly JWTService _jwtService;
		private readonly SignInManager<User> _signInManager;
		private readonly UserManager<User> _userManager;
		private readonly EmailService _emailService;
		private readonly IConfiguration _configuration;

		public ContaController(UserManager<User> userManager,
							   JWTService jwtService,
							   SignInManager<User> signInManager,
							   EmailService emailService,
							   IConfiguration configuration)
		{
			_userManager = userManager;
			_jwtService = jwtService;
			_signInManager = signInManager;
			_emailService = emailService;
			_configuration = configuration;
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
			};

			var result = await _userManager.CreateAsync(userToAdd, registerDto.Password);

			if (!result.Succeeded)
				return BadRequest(result.Errors);

				try
				{
					if(await SendConfirmEMailAsync(userToAdd))
					{
						return Ok(new JsonResult(new {title= "Conta criada", message= "Confirme seu email" }));
					}
					return BadRequest("Falha ao enviar o email");
				}
				catch (Exception ex)
				{
					return BadRequest("Falha ao enviar o email");
				}
			
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

		private async Task<bool> SendConfirmEMailAsync(User user)
		{
			var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
			token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
			var url = $"{_configuration["JWT:ClientUrl"]}/{_configuration["Email:ConfirmEmailPath"]}?token={token}&email={user.Email}";

			var body = $"<p>Hello: {user.FirstName} {user.LastName}</p>" +
				"<p>Confirme seu email clicando no link.</p>" +
				$"<p><a href=\"{url}\">Clique aqui</a></p>" +
				"<p>Obrigado,</p>" +
				$"<br>{_configuration["Email:ApplicationName"]}";

			var emailSend = new EmailSendDto(user.Email, "Confirme seu email", body);

			return await _emailService.SendEmailAsync(emailSend);
		}
	}
}
