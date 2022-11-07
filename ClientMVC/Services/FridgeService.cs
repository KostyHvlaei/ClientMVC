﻿using ClientMVC.Extensions;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ClientMVC.Services
{
	public class FridgeService : IFridgeService
	{
		private readonly HttpClient _client;
		public const string BasePath = "/api/fridges";

		public FridgeService(HttpClient client)
		{
			_client = client;
		}

		public async Task<IEnumerable<FridgeDTO>> GetAll()
		{
			var response = await _client.GetAsync(BasePath);

			return await response.ReadContentAsync<List<FridgeDTO>>();
		}

		public async Task<FridgeProductsDTO> GetFridge(Guid id)
		{
			var response = await _client.GetAsync(BasePath + "/" + id.ToString());
			return await response.ReadContentAsync<FridgeProductsDTO>();
		}

		public async Task<bool> EditFridge(Guid id, FridgeToUpdateDTO fridge)
		{
			var response = await _client.PutAsJsonAsync(BasePath + "/" + id.ToString(), fridge);
			return response.IsSuccessStatusCode;
		}

		public async Task<(bool, Guid)> CreateFridge(FridgeToCreationDTO fridge)
		{
			var response = await _client.PostAsJsonAsync(BasePath, fridge);
			if (!response.IsSuccessStatusCode)
				return (false, Guid.Empty);
			var newFridge = await response.ReadContentAsync<FridgeDTO>();
			return (true, newFridge.Id);
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

		public async Task<bool> DeleteFridge(Guid id)
		{
			var response = await _client.DeleteAsync(BasePath + "/" + id.ToString());
			return response.IsSuccessStatusCode;
		}

		public async Task<string> UpdateFridgeProducts()
		{
			var response = await _client.GetAsync(BasePath + "/UpdateFrigdeProducts");
			if (!response.IsSuccessStatusCode)
				return "";
			return await response.Content.ReadAsStringAsync();
		}

		public async Task<string> RemoveProductFromFridge(Guid fridgeId, Guid productId)
		{
			var response = await _client.DeleteAsync(BasePath + $"/{fridgeId}/" + productId.ToString());
			if (!response.IsSuccessStatusCode)
				return await response.Content.ReadAsStringAsync();
			else
				return "";
		}
	}
}
