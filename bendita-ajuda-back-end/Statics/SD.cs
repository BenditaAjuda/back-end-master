using Microsoft.AspNetCore.Authorization;

namespace bendita_ajuda_back_end.Statics
{
	public static class SD
	{
		// Roles
		public const string AdminRole = "Super";
		public const string ManagerRole = "Gerente";
		public const string UsuarioRole = "Usuario";

		public const string AdminUserName = "benditaajuda6@gmail.com";
		public const string SuperAdminChangeNotAllowed = "Super não pode ser bloqueado";
		public const int MaximumLoginAttempts = 3;

		//public static bool VIPPolicy(AuthorizationHandlerContext context)
		//{
		//	if (context.User.IsInRole(PlayerRole) &&
		//		context.User.HasClaim(c => c.Type == ClaimTypes.Email && c.Value.Contains("vip")))
		//	{
		//		return true;
		//	}

		//	return false;
		//}
	}
}
