using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Contracts;
using Entities.DataTransferObjects;

namespace ClientMVC.Services
{
	public class FridgeProductsService : IFridgeProductsService
	{
		private readonly HttpClient _client;
		public const string BasePath = "/api/fridges";

		public FridgeProductsService(HttpClient client)
		{
			_client = client;
		}

		public async Task<bool> AddProductToFridge(Guid id, ProductToAddInFridgeDTO product)
		{
			var response = await _client.PostAsJsonAsync(BasePath + "/" + id.ToString(), product);
			return response.IsSuccessStatusCode;
		}

		public async Task<bool> UpdateProductInFridge(Guid fridgeId, ProductToUpdateInFridgeDTO product)
		{
			var response = await _client.PutAsJsonAsync(BasePath + "/" + fridgeId.ToString() + "/" + product.ProductId,
				new { quantity = product.Quantity });
			return response.IsSuccessStatusCode;
		}

		public async Task<string> RemoveProductFromFridge(Guid fridgeId, Guid productId)
		{
			var response = await _client.DeleteAsync(BasePath + $"/{fridgeId}/" + productId.ToString());
			if (!response.IsSuccessStatusCode)
				return await response.Content.ReadAsStringAsync();
			else
				return "";
		}

		public async Task<string> UpdateFridgeProducts()
		{
			var response = await _client.GetAsync(BasePath + "/UpdateFrigdeProducts");
			if (!response.IsSuccessStatusCode)
				return "";
			return await response.Content.ReadAsStringAsync();
		}
	}
}
