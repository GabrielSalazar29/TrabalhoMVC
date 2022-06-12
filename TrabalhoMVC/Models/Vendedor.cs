using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace TrabalhoMVC.Models {
	public class Vendedor {
		public int Id { get; set; }

		[Required(ErrorMessage = "{0} Obrigatório")]
		[StringLength(60, MinimumLength = 3, ErrorMessage = "O tamanho do {0} deve ser entre 3 e 60 letras")]
		public string Nome { get; set; }

		[Required(ErrorMessage = "{0} Obrigatório")]
		[EmailAddress(ErrorMessage = "Insira um Email valido")]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }

		[Required(ErrorMessage = "{0} Obrigatório")]
		[Display(Name = "Data de Nascimento")]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
		public DateTime DataNascimento { get; set; }

		[Required(ErrorMessage = "{0} Obrigatório")]
		[Range(100, 1000000, ErrorMessage = "{0} deve ser entre {1} e {2}")]
		[Display(Name = "Salário Base")]
		[DisplayFormat(DataFormatString = "R$ {0:F2}")]
		public double SalarioBase { get; set; }

		public Departamento Departamento { get; set; }
		[Display(Name = "Departamento")]
		public int DepartamentoId { get; set; }

		public ICollection<RegistroVendas> Vendas { get; set; } = new List<RegistroVendas>();

		public Vendedor() {

		}

		public Vendedor(int id, string nome, string email, DateTime dataNascimento, double salarioBase, Departamento departamento) {
			Id = id;
			Nome = nome;
			Email = email;
			DataNascimento = dataNascimento;
			SalarioBase = salarioBase;
			Departamento = departamento;
		}

		public void AddVendas(RegistroVendas rv) {

			Vendas.Add(rv);
		}

		public void RemoveVendas(RegistroVendas rv) {

			Vendas.Remove(rv);
		}

		public double TotalVendas(DateTime inicio, DateTime fim) {

			return Vendas.Where(rv => rv.Data >= inicio && rv.Data <= fim).Sum(rv => rv.Quantia);
		}
	}
}
