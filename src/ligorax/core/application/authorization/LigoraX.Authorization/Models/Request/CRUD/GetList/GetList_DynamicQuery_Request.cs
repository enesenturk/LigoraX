using Base.DataQuery.Search.DynamicSearch.Dtos;
using LigoraX.Authorization.Models.Request.Authorization;

namespace LigoraX.Authorization.Models.Request.CRUD.GetList
{
	public class GetList_DynamicQuery_Request : AuthorizationRequest
	{
		public DynamicQuery DynamicQuery { get; set; }
	}
}
