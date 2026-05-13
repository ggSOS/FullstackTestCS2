using FullstackTestCS2.model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullstackTestCS2
{
    public class HelpDeskContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Tecnico> Tecnicos { get; set; }
        public DbSet<Chamado> Chamados { get; set; }
        public DbSet<Equipamento> Equipamentos { get; set; }

        public HelpDeskContext(DbContextOptions<HelpDeskContext> options)
        : base(options)
        {
        }
    }
}
