namespace byLiGG.Authorization.Models.Context
{
	public class AuthenticatedRequestContext : RequestContext
	{

		public Guid ExecutingUserId { get; set; }

	}

}