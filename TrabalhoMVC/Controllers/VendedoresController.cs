using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrabalhoMVC.Services;

namespace TrabalhoMVC.Controllers {
	public class VendedoresController : Controller {

		private readonly VendedorService _sellerService;

		public VendedoresController(VendedorService sellerService) {

			_sellerService = sellerService;

		}
		public IActionResult Index() {
			var list = _sellerService.FindAll();

			return View(list);
		}
	}
}
