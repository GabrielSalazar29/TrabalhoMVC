using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrabalhoMVC.Data;
using TrabalhoMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace TrabalhoMVC.Services {
	public class DepartamentoService {
		private readonly TrabalhoMVCContext _context;

		public DepartamentoService(TrabalhoMVCContext context) {

			_context = context;
		}

		public async Task<List<Departamento>> FindAllAsync() {

			return await _context.Departamento.OrderBy(x => x.Nome).ToListAsync();
		}
	}
}
