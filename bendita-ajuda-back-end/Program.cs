using bendita_ajuda_back_end.Data;
using bendita_ajuda_back_end.Loggings;
using bendita_ajuda_back_end.Models.User;
using bendita_ajuda_back_end.Repositories.AuthServices;
using bendita_ajuda_back_end.Repositories.Interfaces;
using bendita_ajuda_back_end.Repositories.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using bendita_ajuda_back_end.Repositories.EmailService;
using bendita_ajuda_back_end.Repositories.ContextSeedService;
using bendita_ajuda_back_end.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
	.AddJsonOptions(options =>
	options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<BenditaAjudaDbContext>(options =>
{
	options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

builder.Services.AddScoped<JWTService>();

builder.Services.AddIdentityCore<User>(options =>
{
	options.Password.RequiredLength = 6;
	options.Password.RequireDigit = false;
	options.Password.RequireLowercase = false;
	options.Password.RequireUppercase = false;
	options.Password.RequireNonAlphanumeric = false;

	options.SignIn.RequireConfirmedEmail = true;
})
	.AddRoles<IdentityRole>()
	.AddRoleManager<RoleManager<IdentityRole>>()
	.AddEntityFrameworkStores<BenditaAjudaDbContext>()
	.AddSignInManager<SignInManager<User>>()
	.AddUserManager<UserManager<User>>()
	.AddDefaultTokenProviders();

builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<IPrestadorRepository, PrestadorRepository>();
builder.Services.AddScoped<IServicosMeiRepository, ServicoMeiRepository>();
builder.Services.AddScoped<EmailService>();
builder.Services.AddScoped<ContextSeedService>();

builder.Logging.AddProvider(new CustomLoggerProvider(new CustomLoggerProviderConfiguration
{
	LogLevel = LogLevel.Warning
}));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options =>
	{
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuerSigningKey = true,
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
			ValidIssuer = builder.Configuration["JWT:Issuer"],
			ValidateIssuer = true,
			ValidateAudience = false		};
	});

builder.Services.AddCors();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
	options.InvalidModelStateResponseFactory = actionContext =>
	{
		var errors = actionContext.ModelState
		.Where(x => x.Value.Errors.Count > 0)
		.SelectMany(x => x.Value.Errors)
		.Select(x => x.ErrorMessage).ToArray();

		var toReturn = new
		{
			Errors = errors
		};

		return new BadRequestObjectResult(toReturn);
	};
});

var app = builder.Build();

app.UseCors(options =>
{
	options.AllowAnyHeader().
			AllowAnyMethod().
			AllowCredentials().
			WithOrigins("http://localhost:4200");
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

using var scope = app.Services.CreateScope();
try
{
	var contextSeedService = scope.ServiceProvider.GetService<ContextSeedService>();
	await contextSeedService.InitializeContextAsync();
}
catch (Exception ex)
{
	var logger = scope.ServiceProvider.GetService<ILogger<Program>>();
	logger.LogError(ex.Message, "Falha ao inicializar database");
}

app.Run();
