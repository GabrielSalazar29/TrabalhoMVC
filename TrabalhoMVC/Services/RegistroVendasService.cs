﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrabalhoMVC.Data;
using TrabalhoMVC.Models;

namespace TrabalhoMVC.Services {
	public class RegistroVendasService {

		private readonly TrabalhoMVCContext _context;

		public RegistroVendasService(TrabalhoMVCContext context) {

			_context = context;
		}

		public async Task<List<RegistroVenda>> FindByDateAsync(DateTime? minDate, DateTime? maxDate) {

			var result = from obj in _context.RegistroVendas select obj;
			if (minDate.HasValue) {
				result = result.Where(x => x.Data >= minDate.Value);

			}
			if (maxDate.HasValue) {

				result = result.Where(x => x.Data <= maxDate.Value);

			}
			return await result
				.Include(x => x.Vendedor)
				.Include(x => x.Vendedor.Departamento)
				.OrderByDescending(x => x.Data)
				.ToListAsync();
		}

		public Dictionary<Departamento, List<RegistroVenda>> FindByDateGrouping(DateTime? minDate, DateTime? maxDate) {

			var result = from obj in _context.RegistroVendas select obj;
			if (minDate.HasValue) {
				result = result.Where(x => x.Data >= minDate.Value);

			}
			if (maxDate.HasValue) {

				result = result.Where(x => x.Data <= maxDate.Value);

			}

			var tb = result
				.Include(x => x.Vendedor)
				.Include(x => x.Vendedor.Departamento)
				.OrderByDescending(x => x.Data)
				.ToList()
				.GroupBy(x => x.Vendedor.Departamento)
				.ToDictionary(x => x.Key, x => x.ToList());

			

			return tb;
		}
	}
}
