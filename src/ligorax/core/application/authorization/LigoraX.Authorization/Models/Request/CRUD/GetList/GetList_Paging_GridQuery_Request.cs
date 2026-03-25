using Base.DataQuery.Paging.Request;
using Base.DataQuery.Search.GridSearch.Dtos;
using LigoraX.Authorization.Models.Request.Authorization;

namespace LigoraX.Authorization.Models.Request.CRUD.GetList
{
	public class GetList_Paging_GridQuery_Request : AuthorizationRequest
	{

		public PagingRequest PagingRequest { get; set; }
		public List<GridQuery> GridQueries { get; set; } = new List<GridQuery>();

	}
}