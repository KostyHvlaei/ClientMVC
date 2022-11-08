using ClientMVC.Extensions;
using Contracts;
using Entities.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClientMVC.Services
{
	public class FridgeModelService : IFridgeModelService
	{
		private readonly HttpClient _client;
		public const string BasePath = "/api/models";

		public FridgeModelService(HttpClient client)
		{
			_client = client;
		}

		public async Task<IEnumerable<FridgeModelDTO>> GetAll()
		{
			var response = await _client.GetAsync(BasePath);

			return await response.ReadContentAsync<List<FridgeModelDTO>>();
		}

		public async Task<bool> CreateFridgeModel(FridgeModelToCreationDTO fridgeModel)
		{
			var response = await _client.PostAsJsonAsync(BasePath, fridgeModel);
			return response.IsSuccessStatusCode;
		}

		public async Task<bool> EditFridgeModel(Guid id, FridgeModelToUpdateDTO fridgeModel)
		{
			var response = await _client.PutAsJsonAsync(BasePath + "/" + id.ToString(), fridgeModel);
			return response.IsSuccessStatusCode;
		}

		public async Task<FridgeModelDTO> GetFridge(Guid id)
		{
			var response = await _client.GetAsync(BasePath + "/" + id.ToString());
			return await response.ReadContentAsync<FridgeModelDTO>();
		}
	}
}
