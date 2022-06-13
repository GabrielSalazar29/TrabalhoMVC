using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrabalhoMVC.Models;

namespace TrabalhoMVC.Data
{
    public class TrabalhoMVCContext : DbContext
    {
        public TrabalhoMVCContext (DbContextOptions<TrabalhoMVCContext> options)
            : base(options)
        {
        }

        public DbSet<Departamento> Departamento { get; set; }
        public DbSet<Vendedor> Vendedores { get; set; }
        public DbSet<RegistroVenda> RegistroVendas { get; set; }
    }
}
