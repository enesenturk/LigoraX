using Base.DataQuery.Paging.Request;
using Base.DataQuery.Search.DynamicSearch.Dtos;
using LigoraX.Authorization.Models.Request.Authorization;

namespace LigoraX.Authorization.Models.Request.CRUD.GetList
{
	public class GetList_Paging_DynamicQuery_Request : AuthorizationRequest
	{

		public PagingRequest PagingRequest { get; set; }
		public DynamicQuery DynamicQuery { get; set; }

	}
}
