namespace byLiGG.Authorization.Models.Context.Authorization
{
	public class AuthorizationTicket
	{

		public Guid UserId { get; set; }
		public Guid RoleId { get; set; }
		public DateTime Timeout { get; set; }

	}

}