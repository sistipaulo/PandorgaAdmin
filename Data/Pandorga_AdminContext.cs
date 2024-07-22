using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pandorga_Admin.Models;

namespace Pandorga_Admin.Data
{
    public class Pandorga_AdminContext : DbContext
    {
        public Pandorga_AdminContext (DbContextOptions<Pandorga_AdminContext> options)
            : base(options)
        {
        }

        public DbSet<Pandorga_Admin.Models.Aluno> Aluno { get; set; } = default!;

        public DbSet<Pandorga_Admin.Models.Professor>? Professor { get; set; }

        public DbSet<Pandorga_Admin.Models.Evento>? Evento { get; set; }

        public DbSet<Pandorga_Admin.Models.Sala>? Sala { get; set; }

        public DbSet<Pandorga_Admin.Models.Turma>? Turma { get; set; }
    }
}
