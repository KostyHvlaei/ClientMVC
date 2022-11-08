using Entities.DataTransferObjects;
using System;
using System.Collections.Generic;

namespace ClientMVC.ViewModels
{
    public class ProductToAddInFridgeViewModel
    {
        public Guid FridgeId { get; set; }
        public IEnumerable<ProductDTO> Products { get; set; }
        public Guid productId { get; set; }
        public int Quantity { get; set; }
    }
}