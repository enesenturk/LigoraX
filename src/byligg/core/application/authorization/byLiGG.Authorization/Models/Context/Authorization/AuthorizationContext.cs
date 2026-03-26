namespace byLiGG.Authorization.Models.Context.Authorization
{
	public class AuthorizationContext : AuthenticatedRequestContext
	{

		public AuthorizationTicket Ticket { get; set; }

	}
}