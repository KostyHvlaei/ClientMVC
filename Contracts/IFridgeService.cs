using Entities.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
	public interface IFridgeService
	{
		public Task<IEnumerable<FridgeDTO>> GetAll();
		Task<FridgeProductsDTO> GetFridge(Guid id);
		Task<bool> EditFridge(Guid id, FridgeToUpdateDTO fridge);
		Task<(bool, Guid)> CreateFridge(FridgeToCreationDTO fridge);
		Task<bool> DeleteFridge(Guid id);
	}
}
