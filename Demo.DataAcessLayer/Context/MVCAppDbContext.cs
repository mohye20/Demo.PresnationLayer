using Demo.DataAcessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DataAcessLayer.Context
{
    public class MVCAppDbContext : DbContext
    {
        public MVCAppDbContext(DbContextOptions<MVCAppDbContext> options) : base(options)
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //=>    optionsBuilder.UseSqlServer("Server = . ; Database = MVCAppDb ; trusted_Connection = true ");

        public DbSet<Department> Departmnets { get; set; }

        public DbSet<Employee> Employees { get; set; }
    }
}