using Entities.DataTransferObjects;
using System.Collections.Generic;

namespace ClientMVC.ViewModels
{
    public class CreateFridgeViewModel
    {
        public FridgeToCreationDTO Fridge { get; set; }
        public IEnumerable<FridgeModelDTO> FridgeModel { get; set; }
        public List<ProductToAddInFridgeWNDTO> Products { get; set; }
    }
}
