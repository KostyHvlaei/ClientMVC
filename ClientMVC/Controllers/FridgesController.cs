using ClientMVC.Services;
using ClientMVC.ViewModels;
using Contracts;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientMVC.Controllers
{
	public class FridgesController : Controller
	{
		private readonly IFridgeService _service;
		private readonly IFridgeModelService _modelService;
		private readonly IProductService _productService;
		private readonly IFridgeProductsService _fridgeProductsService;

		public FridgesController(IFridgeService service
			, IFridgeModelService modelService
			, IProductService productService
			, IFridgeProductsService fridgeProductsService)
		{
			_service = service;
			_modelService = modelService;
			_productService = productService;
			_fridgeProductsService = fridgeProductsService;
		}

		// GET: FridgesController
		public async Task<ActionResult> Index()
		{
			var content = await _service.GetAll();
			return View(content);
		}

		// GET: FridgesController/Details/5
		public async Task<ActionResult> Details(Guid id)
		{
			var fridge_with_prods = await _service.GetFridge(id);
			if (fridge_with_prods == null)
				return Redirect("Index");
			return View(fridge_with_prods);
		}

		// GET: FridgesController/Create
		public async Task<ActionResult> Create()
		{
			var ivm = new CreateFridgeViewModel();
			var models = await _modelService.GetAll();
			var products_f = await _productService.GetAll();
			var products = new List<ProductToAddInFridgeWNDTO>();
			foreach (var item in products_f)
				products.Add(new ProductToAddInFridgeWNDTO
				{
					ProductId = item.Id,
					Name = item.Name,
					Quantity = 0,
					Changed = false
				});
			ivm.Products = products;
			ivm.FridgeModel = models;

			return View(ivm);
		}

		// POST: FridgesController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create([FromForm] FridgeToCreateWithProductsDTO fridge_to_create)
		{
			if (ModelState.IsValid)
			{
				var frid_to_put = fridge_to_create.Fridge;
				var (created, id) = await _service.CreateFridge(frid_to_put);
				if (!created)
				{
					ModelState.AddModelError("Error", "Something went wrong");
					return RedirectToAction("Create");
				}

				foreach (var prod in fridge_to_create.Products)
				{
					if (prod.Changed && prod.Quantity > 0)
					{
						var added = await _fridgeProductsService.AddProductToFridge(id, new ProductToAddInFridgeDTO
						{
							ProductId = prod.ProductId,
							Quantity = prod.Quantity
						});
					}
				}

				return RedirectToAction("Index");
			}

			return View(fridge_to_create);
		}

		// GET: FridgesController/Edit/5
		public async Task<ActionResult> Edit(Guid id)
		{
			var fridge = await _service.GetFridge(id);
			if (fridge == null)
				return RedirectToAction("Index");
			var fridgeToUpdate = new FridgeDTO { Id = fridge.FridgeId, Name = fridge.FridgeName, OwnerName = fridge.OwnerName };
			return View(fridgeToUpdate);
		}

		// POST: FridgesController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit(Guid id, [FromForm] FridgeToUpdateDTO fridge)
		{
			if (ModelState.IsValid)
			{
				bool success = await _service.EditFridge(id, fridge);
				if (success)
					return RedirectToAction("Index");
				else
					ModelState.AddModelError("Model", "Error in model");
			}

			var fridgeToUpdate = new FridgeDTO { Id = id, Name = fridge.Name, OwnerName = fridge.OwnerName };

			return View(fridgeToUpdate);
		}

		// GET: FridgesController/Delete/5
		public ActionResult Delete(Guid id)
		{
			return View(new DeleteViewModel { Id = id, Agree = false});
		}

		// POST: FridgesController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Delete(Guid id, [FromForm]bool Agree)
		{
			var res = false;
			if (Agree)
				res = await _service.DeleteFridge(id);

			return RedirectToAction("Index");
		}

		[HttpGet]
		public async Task<ActionResult> UpdateProducts()
		{
			var res = await _fridgeProductsService.UpdateFridgeProducts();
			if (string.IsNullOrEmpty(res))
				res = "Error occuped";
			return View("UpdateProducts", res);
		}

		[HttpGet]
		public async Task<ActionResult> AddProductInFridge(Guid fridgeId)
		{
			ProductToAddInFridgeViewModel vm = new ProductToAddInFridgeViewModel
			{
				FridgeId = fridgeId,
				Products = await _productService.GetAll()
			};

			return View(vm);
		}

		[HttpPost]
		public async Task<ActionResult> AddProductInFridge(Guid fridgeId, [FromForm] ProductToAddInFridgeDTO productToAdd)
		{
			if(!ModelState.IsValid)
			{
				return View(new ProductToAddInFridgeViewModel
				{
					FridgeId = fridgeId,
					Products = await _productService.GetAll()
				});
			}

			var isSuccess = await _fridgeProductsService.AddProductToFridge(fridgeId, productToAdd);
			if (isSuccess)
				return RedirectToRoute("default", new {controller = "Fridges", action = "details", id = fridgeId});
			else
			{
				ModelState.AddModelError("Add product", "Something went wrong");
				return View(new ProductToAddInFridgeViewModel
				{
					FridgeId = fridgeId,
					Products = await _productService.GetAll()
				});
			}
		}

		[HttpPost]
		public async Task<ActionResult> UpdateProductInFridge(Guid fridgeId, [FromForm] ProductToUpdateInFridgeDTO product)
		{
			if (ModelState.IsValid)
			{
				var isSuccess = await _fridgeProductsService.UpdateProductInFridge(fridgeId, product);
				return RedirectToAction("Details", new { id = fridgeId });
			}
			var fridge_with_prods = await _service.GetFridge(fridgeId);
			return View("Details", fridge_with_prods);
		}

		[HttpGet]
		public async Task<IActionResult> RemoveProductFromFridge(Guid fridgeId, Guid productId)
		{
			var resp = await _fridgeProductsService.RemoveProductFromFridge(fridgeId, productId);
			if (string.IsNullOrEmpty(resp))
				return RedirectToAction("Details", new { id = fridgeId });
			return View(resp);
		}
	}
}
