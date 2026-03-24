using Base.RestApi.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Base.RestApi
{
	public class RestApiRequest
	{

		public static RestfulServiceResponseDto Delete(string URL, object dataModel, List<RestfulServiceHeaderDto> headers)
		{
			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				foreach (RestfulServiceHeaderDto header in headers)
					client.DefaultRequestHeaders.Add(header.Name, header.Value);

				StringContent stringContent = null;

				if (dataModel is not null)
				{
					string content = JsonConvert.SerializeObject(dataModel);
					stringContent = new StringContent(content, Encoding.UTF8, "application/json");
				}

				HttpRequestMessage request = new HttpRequestMessage
				{
					Method = HttpMethod.Delete,
					RequestUri = new Uri(URL),
					Content = stringContent
				};

				HttpResponseMessage response =  client.Send(request);
				string result = response.Content.ReadAsStringAsync().Result;

				return new RestfulServiceResponseDto
				{
					Response = result,
					StatusCode = response.StatusCode
				};
			}
		}

		public static RestfulServiceResponseDto Get(string URL, string parameters, List<RestfulServiceHeaderDto> headers)
		{
			using (HttpClient client = new HttpClient())
			{
				client.BaseAddress = new Uri(URL);
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				foreach (RestfulServiceHeaderDto header in headers)
					client.DefaultRequestHeaders.Add(header.Name, header.Value);

				HttpResponseMessage response = client.GetAsync(parameters).Result;
				string result = response.Content.ReadAsStringAsync().Result;

				return new RestfulServiceResponseDto
				{
					Response = result,
					StatusCode = response.StatusCode
				};
			}
		}

		public static RestfulServiceResponseDto Post(string URL, object dataModel)
		{
			using (HttpClient client = new HttpClient())
			{
				client.BaseAddress = new Uri(URL);
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				string content = JsonConvert.SerializeObject(dataModel);
				StringContent stringContent = new StringContent(content, Encoding.UTF8, "application/json");

				HttpResponseMessage response = client.PostAsync(URL, stringContent).Result;

				string result = response.Content.ReadAsStringAsync().Result;

				return new RestfulServiceResponseDto
				{
					Response = result,
					StatusCode = response.StatusCode
				};
			}
		}

		public static RestfulServiceResponseDto Post(string URL, object dataModel, List<RestfulServiceHeaderDto> headers)
		{
			using (HttpClient client = new HttpClient())
			{
				client.BaseAddress = new Uri(URL);
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				foreach (RestfulServiceHeaderDto header in headers)
					client.DefaultRequestHeaders.Add(header.Name, header.Value);

				string content = JsonConvert.SerializeObject(dataModel);
				StringContent stringContent = new StringContent(content, Encoding.UTF8, "application/json");

				HttpResponseMessage response = client.PostAsync(URL, stringContent).Result;

				string result = response.Content.ReadAsStringAsync().Result;

				return new RestfulServiceResponseDto
				{
					Response = result,
					StatusCode = response.StatusCode
				};
			}
		}

		public static RestfulServiceResponseDto Put(string URL, object dataModel, List<RestfulServiceHeaderDto> headers)
		{
			using (HttpClient client = new HttpClient())
			{
				client.BaseAddress = new Uri(URL);
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				foreach (RestfulServiceHeaderDto header in headers)
					client.DefaultRequestHeaders.Add(header.Name, header.Value);

				string content = JsonConvert.SerializeObject(dataModel);
				StringContent stringContent = new StringContent(content, Encoding.UTF8, "application/json");

				HttpResponseMessage response = client.PutAsync(URL, stringContent).Result;

				string result = response.Content.ReadAsStringAsync().Result;

				return new RestfulServiceResponseDto
				{
					Response = result,
					StatusCode = response.StatusCode
				};
			}
		}

	}
}
