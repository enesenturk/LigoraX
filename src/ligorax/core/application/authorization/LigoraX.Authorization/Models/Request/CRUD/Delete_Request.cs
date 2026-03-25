using LigoraX.Authorization.Models.Request.Authorization;

namespace LigoraX.Authorization.Models.Request.CRUD
{
	public class Delete_Request : AuthorizationRequest
	{

		public int RecordId { get; set; }
		public bool IsSoftBlockApproved { get; set; }

	}
}
