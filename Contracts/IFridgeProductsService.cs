using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.DataTransferObjects;

namespace Contracts
{
	public interface IFridgeProductsService
	{
		Task<bool> AddProductToFridge(Guid id, ProductToAddInFridgeDTO product);
		Task<bool> UpdateProductInFridge(Guid id, ProductToUpdateInFridgeDTO product);
		Task<string> RemoveProductFromFridge(Guid fridgeId, Guid productId);
		Task<string> UpdateFridgeProducts();
	}
}
