namespace byLiGG.Authorization.Models.Context.Authorization
{
	public class AuthorizationFreeContext : AuthenticatedRequestContext
	{

		public AuthorizationFreeContext(Guid executingUserId, string ipAddress)
		{
			ExecutingUserId = executingUserId;
			IpAddress = ipAddress;
		}

	}
}