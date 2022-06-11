using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrabalhoMVC.Data;
using TrabalhoMVC.Models;
using TrabalhoMVC.Services.Exceptions;

namespace TrabalhoMVC.Services {
	public class VendedorService {

		private readonly TrabalhoMVCContext _context;

		public VendedorService(TrabalhoMVCContext context) {

			_context = context;
		}

		public List<Vendedor> FindAll() {


			return _context.Vendedores.ToList();
		}

		public void Insert(Vendedor vendedor) {
			_context.Add(vendedor);
			_context.SaveChanges();
		}

		public Vendedor FindById(int id) {


			return _context.Vendedores.Include(x => x.Departamento).FirstOrDefault(x => x.Id == id);
		}

		public void Remove(int id) {

			var vendedor = _context.Vendedores.Find(id);

			_context.Vendedores.Remove(vendedor);
			_context.SaveChanges();
		}

		public void Update(Vendedor vendedor) {

			if (!_context.Vendedores.Any(x => x.Id == vendedor.Id)) {

				throw new NotFoundException("Id não encontrado");

			}
			try {

			_context.Update(vendedor);
			_context.SaveChanges();

			}catch(DbUpdateConcurrencyException e) {
				throw new DbConcurrencyException(e.Message);
			}
		}
	}
}
