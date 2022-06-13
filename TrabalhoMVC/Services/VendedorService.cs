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

		public async Task<List<Vendedor>> FindAllAsync() {


			return await _context.Vendedores.ToListAsync();
		}

		public async Task InsertAsync(Vendedor vendedor) {
			_context.Add(vendedor);

			await _context.SaveChangesAsync();
		}

		public async Task<Vendedor> FindByIdAsync(int id) {


			return await _context.Vendedores.Include(x => x.Departamento).FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task RemoveAsync(int id) {

			try {
				var vendedor = await _context.Vendedores.FindAsync(id);

				_context.Vendedores.Remove(vendedor);

				await _context.SaveChangesAsync();

			} catch (DbUpdateException e) {

				throw new IntegrityException("Não é possivel deletar o vendedor, pois ele(a) possui vendas.");
			}

			
		}

		public async Task UpdateAsync(Vendedor vendedor) {

			if (!(await _context.Vendedores.AnyAsync(x => x.Id == vendedor.Id))) {

				throw new NotFoundException("Id não encontrado");

			}
			try {

				_context.Update(vendedor);
				await _context.SaveChangesAsync();

			}catch(DbUpdateConcurrencyException e) {
				throw new DbConcurrencyException(e.Message);
			}
		}
	}
}
