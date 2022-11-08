using Entities.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
	public interface IProductService
	{
		public Task<IEnumerable<ProductDTO>> GetAll();
		Task<bool> CreateProduct(ProductToCreationDTO fridgeModel);
		Task<bool> EditProduct(Guid id, ProductToUpdateDTO fridgeModel);
		Task<ProductDTO> GetProduct(Guid id);
	}
}
