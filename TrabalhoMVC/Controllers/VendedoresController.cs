using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrabalhoMVC.Models;
using TrabalhoMVC.Models.ViewModels;
using TrabalhoMVC.Services;

namespace TrabalhoMVC.Controllers {
	public class VendedoresController : Controller {

		private readonly VendedorService _sellerService;
		private readonly DepartamentoService _departamentoService;


		public VendedoresController(VendedorService sellerService, DepartamentoService departamentoService) {

			_sellerService = sellerService;
			_departamentoService = departamentoService;

		}
		public IActionResult Index() {
			var list = _sellerService.FindAll();

			return View(list);
		}

		public IActionResult Create() {

			var departamentos = _departamentoService.FindAll();
			var viewModel = new VendedorFormViewModel { Departamentos = departamentos };
			return View(viewModel);
		}

		[HttpPost]
		public IActionResult Create(Vendedor vendedor) {

			_sellerService.Insert(vendedor);

			return RedirectToAction(nameof(Index));
		}
	}
}
