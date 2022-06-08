﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrabalhoMVC.Models;

namespace TrabalhoMVC.Controllers {
	public class DepartamentosController : Controller {
		public IActionResult Index() {

			List<Departamento> list = new List<Departamento>();
			list.Add(new Departamento { Id = 1, Nome = "Eletronicos" });
			list.Add(new Departamento { Id = 2, Nome = "Moda" });


			return View(list);
		}
	}
}