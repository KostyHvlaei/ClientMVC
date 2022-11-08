using ClientMVC.Services;
using Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ClientMVC.Extensions
{
	public static class ServicesExtensions
	{
		public static void ConfigureHttpClients(this IServiceCollection services, IConfiguration conf)
		{
			var baseAdress = new Uri(conf.GetSection("ServerUri").Value);
			services.AddHttpClient<IFridgeModelService, FridgeModelService>(c =>
				c.BaseAddress = baseAdress);
			services.AddHttpClient<IFridgeService, FridgeService>(c =>
				c.BaseAddress = baseAdress);
			services.AddHttpClient<IProductService, ProductService>(c =>
				c.BaseAddress = baseAdress);
			services.AddHttpClient<IFridgeProductsService, FridgeProductsService>(c =>
				c.BaseAddress = baseAdress);
		}
	}
}
