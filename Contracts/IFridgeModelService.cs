using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.DataTransferObjects;

namespace Contracts
{
	public interface IFridgeModelService
	{
		Task<IEnumerable<FridgeModelDTO>> GetAll();
		Task<bool> CreateFridgeModel(FridgeModelToCreationDTO fridgeModel);
		Task<bool> EditFridgeModel(Guid id, FridgeModelToUpdateDTO fridgeModel);
		Task<FridgeModelDTO> GetFridge(Guid id);
	}
}
