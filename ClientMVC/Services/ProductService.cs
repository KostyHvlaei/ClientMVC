using ClientMVC.Extensions;
using Contracts;
using Entities.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ClientMVC.Services
{
	public class ProductService : IProductService
	{
		private readonly HttpClient _client;
		public const string BasePath = "/api/products";

		public ProductService(HttpClient client)
		{
			_client = client;
		}

		public async Task<IEnumerable<ProductDTO>> GetAll()
		{
			var response = await _client.GetAsync(BasePath);

			return await response.ReadContentAsync<List<ProductDTO>>();
		}

		public async Task<bool> CreateProduct(ProductToCreationDTO product)
		{
			var response = await _client.PostAsJsonAsync(BasePath, product);
			return response.IsSuccessStatusCode;
		}

		public async Task<bool> EditProduct(Guid id, ProductToUpdateDTO fridgeModel)
		{
			var response = await _client.PutAsJsonAsync(BasePath + "/" + id.ToString(), fridgeModel);
			return response.IsSuccessStatusCode;
		}
		public async Task<ProductDTO> GetProduct(Guid id)
		{
			var response = await _client.GetAsync(BasePath + "/" + id.ToString());
			return await response.ReadContentAsync<ProductDTO>();
		}
	}
}
