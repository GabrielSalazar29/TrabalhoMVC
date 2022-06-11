using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TrabalhoMVC.Models;
using TrabalhoMVC.Models.ViewModels;
using TrabalhoMVC.Services;
using TrabalhoMVC.Services.Exceptions;

namespace TrabalhoMVC.Controllers {
	public class VendedoresController : Controller {

		private readonly VendedorService _vendedorService;
		private readonly DepartamentoService _departamentoService;


		public VendedoresController(VendedorService vendedorService, DepartamentoService departamentoService) {

			_vendedorService = vendedorService;
			_departamentoService = departamentoService;

		}
		public IActionResult Index() {
			var list = _vendedorService.FindAll();

			return View(list);
		}

		public IActionResult Create() {

			var departamentos = _departamentoService.FindAll();
			var viewModel = new VendedorFormViewModel { Departamentos = departamentos };
			return View(viewModel);
		}

		[HttpPost]
		public IActionResult Create(Vendedor vendedor) {

			_vendedorService.Insert(vendedor);

			return RedirectToAction(nameof(Index));
		}

		public IActionResult Delete(int? id) {

			if (id == null) {

				return RedirectToAction(nameof(Error), new {message = "Id não fornecido" });
			}
			var vendedor = _vendedorService.FindById(id.Value);
			if (vendedor == null) {

				return RedirectToAction(nameof(Error), new { message = "Id não encontrado" });
			}

			return View(vendedor);
		}

		[HttpPost]
		public IActionResult Delete(int id) {


			_vendedorService.Remove(id);

			return RedirectToAction(nameof(Index));
		}

		public IActionResult Details(int? id) {

			if (id == null) {

				return RedirectToAction(nameof(Error), new { message = "Id não fornecido" });
			}
			var vendedor = _vendedorService.FindById(id.Value);
			if (vendedor == null) {

				return RedirectToAction(nameof(Error), new { message = "Id não encontrado" });
			}

			return View(vendedor);
		}

		public IActionResult Edit(int? id) {

			if (id == null) {

				return RedirectToAction(nameof(Error), new { message = "Id não fornecido" });
			}
			var vendedor = _vendedorService.FindById(id.Value);
			if (vendedor == null) {

				return RedirectToAction(nameof(Error), new { message = "Id não encontrado" });
			}

			List<Departamento> departamentos = _departamentoService.FindAll();
			VendedorFormViewModel viewModel = new VendedorFormViewModel { Vendedor = vendedor, Departamentos = departamentos };
			return View(viewModel);
		}

		[HttpPost]
		public IActionResult Edit(int id, Vendedor vendedor) {

			if(id != vendedor.Id) {
				return RedirectToAction(nameof(Error), new { message = "Id não são correspondentes" });
			}

			try {
			_vendedorService.Update(vendedor);
			return RedirectToAction(nameof(Index));

			} catch (ApplicationException e){

				return RedirectToAction(nameof(Error), new { message = e.Message });

			}
		}
		public IActionResult Error(string message) {

			var viewModel = new ErrorViewModel { Message = message, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier};

			return View(viewModel);
		}
	}
}
