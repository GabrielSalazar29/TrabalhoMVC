﻿using System;
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

        public DbSet<TrabalhoMVC.Models.Departamento> Departamento { get; set; }
    }
}