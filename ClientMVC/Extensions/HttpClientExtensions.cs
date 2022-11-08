using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClientMVC.Extensions
{
	public static class HttpClientExtensions
	{
		public static async Task<T> ReadContentAsync<T> (this HttpResponseMessage response)
		{
			if(response.IsSuccessStatusCode == false)
			{
				
			}

			var dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
			var result = JsonSerializer.Deserialize<T>(
				dataAsString, new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true,
				});

			return result;
		}
	}
}
