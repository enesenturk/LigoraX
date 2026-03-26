using Base.DataQuery.Search.GridSearch.Dtos;
using byLiGG.Authorization.Models.Request.Authorization;

namespace byLiGG.Authorization.Models.Request.CRUD.GetList
{
	public class GetList_GridQuery_Request : AuthorizationRequest
	{

		public List<GridQuery> GridQueries { get; set; } = new List<GridQuery>();

	}
}
