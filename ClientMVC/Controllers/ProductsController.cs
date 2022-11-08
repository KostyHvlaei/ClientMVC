using ClientMVC.Services;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ClientMVC.Controllers
{
	public class ProductsController : Controller
	{
		private readonly IProductService _service;

		public ProductsController(IProductService service)
		{
			_service = service;
		}

		// GET: ProductsControllerController
		public async Task<ActionResult> Index()
		{
			var content = await _service.GetAll();
			return View(content);
		}

		//// GET: ProductsControllerController/Details/5
		//public ActionResult Details(int id)
		//{
		//	return View();
		//}

		// GET: ProductsControllerController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: ProductsControllerController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create([FromForm] ProductToCreationDTO product)
		{
			if (ModelState.IsValid)
			{
				bool success = await _service.CreateProduct(product);
				if (success)
					return RedirectToAction("Index");
				else
					ModelState.AddModelError("Model", "Error in model");
			}
			return View(product);
		}

		// GET: ProductsControllerController/Edit/5
		public async Task<ActionResult> Edit(Guid id)
		{
			var product = await _service.GetProduct(id);
			if (product == null)
				return RedirectToAction("Index");
			return View(product);
		}

		// POST: ProductsControllerController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit(Guid id, [FromForm] ProductToUpdateDTO product)
		{
			if (ModelState.IsValid)
			{
				bool success = await _service.EditProduct(id, product);
				if (success)
					return RedirectToAction("Index");
				else
					ModelState.AddModelError("Model", "Error in model");
			}
			return View(product);
		}

		//// GET: ProductsControllerController/Delete/5
		//public ActionResult Delete(int id)
		//{
		//	return View();
		//}

		//// POST: ProductsControllerController/Delete/5
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
