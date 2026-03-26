using byLiGG.Authorization.Models.Request.Authorization;

namespace byLiGG.Authorization.Models.Request.CRUD
{
	public class Get_Request<T> : AuthorizationRequest where T : class, new()
	{
		public T Common { get; set; }
	}

	public class Get_Request : AuthorizationRequest
	{

		public int RecordId { get; set; }

	}
}
