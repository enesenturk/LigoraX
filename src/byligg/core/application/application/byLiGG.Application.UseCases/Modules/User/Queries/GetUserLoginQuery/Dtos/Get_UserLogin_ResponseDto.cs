namespace byLiGG.Application.UseCases.Modules.User.Queries.GetUserLoginQuery.Dtos
{
	public class Get_UserLogin_ResponseDto
	{
		public Guid UserId { get; set; }
		public string Username { get; set; }
		public string Email { get; set; }
		public string DisplayName { get; set; }
		public string Token { get; set; }
	}
}
