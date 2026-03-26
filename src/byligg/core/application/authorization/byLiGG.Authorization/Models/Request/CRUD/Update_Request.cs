using byLiGG.Authorization.Models.Request.Authorization;

namespace byLiGG.Authorization.Models.Request.CRUD
{

	public class Update_Request<T> : AuthorizationRequest
	{
		public int RecordId { get; set; }
		public T Common { get; set; }
	}

	public class Update_Request : AuthorizationRequest
	{
		public int RecordId { get; set; }
	}

}
