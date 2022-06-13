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
		public async Task<IActionResult> Index() {
			var list = await _vendedorService.FindAllAsync();

			return View(list);
		}

		public async Task<IActionResult> Create() {

			var departamentos = await _departamentoService.FindAllAsync();
			var viewModel = new VendedorFormViewModel { Departamentos = departamentos };
			return View(viewModel);
		}

		[HttpPost]
		public async Task<IActionResult> Create(Vendedor vendedor) {

			if (!ModelState.IsValid) {

				var departaments =await _departamentoService.FindAllAsync();
				var viewModel = new VendedorFormViewModel { Vendedor = vendedor, Departamentos = departaments };
				return View(viewModel);

			}

			await _vendedorService.InsertAsync(vendedor);
			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> Delete(int? id) {

			if (id == null) {

				return RedirectToAction(nameof(Error), new {message = "Id não fornecido" });
			}
			var vendedor = await _vendedorService.FindByIdAsync(id.Value);
			if (vendedor == null) {

				return RedirectToAction(nameof(Error), new { message = "Id não encontrado" });
			}

			return View(vendedor);
		}

		[HttpPost]
		public async Task<IActionResult> Delete(int id) {

			try {

			await _vendedorService.RemoveAsync(id);

			return RedirectToAction(nameof(Index));
			} catch (IntegrityException e) {

				return RedirectToAction(nameof(Error), new { message = e.Message }) ;
			}
		}

		public async Task<IActionResult> Details(int? id) {

			if (id == null) {

				return RedirectToAction(nameof(Error), new { message = "Id não fornecido" });
			}
			var vendedor =await _vendedorService.FindByIdAsync(id.Value);
			if (vendedor == null) {

				return RedirectToAction(nameof(Error), new { message = "Id não encontrado" });
			}

			return View(vendedor);
		}

		public async Task<IActionResult> Edit(int? id) {

			if (id == null) {

				return RedirectToAction(nameof(Error), new { message = "Id não fornecido" });
			}
			var vendedor = await _vendedorService.FindByIdAsync(id.Value);
			if (vendedor == null) {

				return RedirectToAction(nameof(Error), new { message = "Id não encontrado" });
			}

			List<Departamento> departamentos =await _departamentoService.FindAllAsync();
			VendedorFormViewModel viewModel = new VendedorFormViewModel { Vendedor = vendedor, Departamentos = departamentos };
			return View(viewModel);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(int id, Vendedor vendedor) {

			if (!ModelState.IsValid) {

				var departaments = await _departamentoService.FindAllAsync();
				var viewModel = new VendedorFormViewModel { Vendedor = vendedor, Departamentos = departaments };
				return View(viewModel);

			}

			if (id != vendedor.Id) {
				return RedirectToAction(nameof(Error), new { message = "Id não são correspondentes" });
			}

			try {
			await _vendedorService.UpdateAsync(vendedor);
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
