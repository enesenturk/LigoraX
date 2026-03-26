using byLiGG.Authorization.Models.Request.Authorization;

namespace byLiGG.Authorization.Models.Request.CRUD
{

	public class Create_Request<T> : AuthorizationRequest where T : class, new()
	{
		public T Common { get; set; }
	}

	public class Create_Request : AuthorizationRequest
	{

	}

}