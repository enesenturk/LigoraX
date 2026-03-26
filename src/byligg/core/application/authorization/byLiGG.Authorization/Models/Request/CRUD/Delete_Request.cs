using byLiGG.Authorization.Models.Request.Authorization;

namespace byLiGG.Authorization.Models.Request.CRUD
{
	public class Delete_Request : AuthorizationRequest
	{

		public int RecordId { get; set; }
		public bool IsSoftBlockApproved { get; set; }

	}
}
