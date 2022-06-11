using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrabalhoMVC.Data;
using TrabalhoMVC.Models;

namespace TrabalhoMVC.Services {
	public class VendedorService {

		private readonly TrabalhoMVCContext _context;

		public VendedorService(TrabalhoMVCContext context) {

			_context = context;
		}

		public List<Vendedor> FindAll() {


			return _context.Vendedores.ToList();
		}
	}
}
