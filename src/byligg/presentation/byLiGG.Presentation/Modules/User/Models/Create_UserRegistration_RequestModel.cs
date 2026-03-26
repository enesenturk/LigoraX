namespace byLiGG.Presentation.Modules.User.Models
{
	public class Create_UserRegistration_RequestModel
	{
		public string Username { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string DisplayName { get; set; }
		public string LanguagePreference { get; set; }
	}
}