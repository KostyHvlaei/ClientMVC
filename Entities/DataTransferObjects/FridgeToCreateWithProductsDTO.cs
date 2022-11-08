using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class FridgeToCreateWithProductsDTO
    {
		public FridgeToCreationDTO Fridge { get; set; }
		public List<ProductToAddInFridgeWNDTO> Products { get; set; }
	}
}
