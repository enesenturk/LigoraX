using Base.DataQuery.Search.DynamicSearch.Dtos;
using byLiGG.Authorization.Models.Request.Authorization;

namespace byLiGG.Authorization.Models.Request.CRUD.GetList
{
	public class GetList_DynamicQuery_Request : AuthorizationRequest
	{
		public DynamicQuery DynamicQuery { get; set; }
	}
}
