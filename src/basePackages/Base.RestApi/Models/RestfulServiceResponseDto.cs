using System.Net;

namespace Base.RestApi.Models
{
	public class RestfulServiceResponseDto
	{

		public string Response { get; set; }
		public HttpStatusCode StatusCode { get; set; }

	}
}