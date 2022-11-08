using ClientMVC.Services;
using Contracts;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ClientMVC.Controllers
{
	public class FridgeModelsController : Controller
	{
		private readonly IFridgeModelService _service;

		public FridgeModelsController(IFridgeModelService service)
		{
			_service = service;
		}

		// GET: FridgeModelsController
		public async Task<ActionResult> Index()
		{
			var content = await _service.GetAll();
			return View(content);
		}

		//// GET: FridgeModelsController/Details/5
		//public ActionResult Details(int id)
		//{
		//	return View();
		//}

		// GET: FridgeModelsController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: FridgeModelsController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create([FromForm] FridgeModelToCreationDTO fridgeModel)
		{
			if (ModelState.IsValid)
			{
				bool success = await _service.CreateFridgeModel(fridgeModel);
				if (success)
					return RedirectToAction("Index");
				else
					ModelState.AddModelError("Model", "Error in model");
			}
			return View(fridgeModel);
		}

		// GET: FridgeModelsController/Edit/5
		public async Task<ActionResult> Edit(Guid id)
		{
			var fridgeModel = await _service.GetFridge(id);
			if (fridgeModel == null)
				return RedirectToAction("Index");
			return View(fridgeModel);
		}

		// POST: FridgeModelsController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit(Guid id, [FromForm] FridgeModelToUpdateDTO fridgeModel)
		{
			if (ModelState.IsValid)
			{
				bool success = await _service.EditFridgeModel(id, fridgeModel);
				if (success)
					return RedirectToAction("Index");
				else
					ModelState.AddModelError("Model", "Error in model");
			}
			return View(fridgeModel);
		}

		//// GET: FridgeModelsController/Delete/5
		//public ActionResult Delete(int id)
		//{
		//	return View();
		//}

		//// POST: FridgeModelsController/Delete/5
		//[HttpPost]
		//[ValidateAntiForgeryToken]
		//public ActionResult Delete(int id, IFormCollection collection)
		//{
		//	try
		//	{
		//		return RedirectToAction(nameof(Index));
		//	}
		//	catch
		//	{
		//		return View();
		//	}
		//}
	}
}
