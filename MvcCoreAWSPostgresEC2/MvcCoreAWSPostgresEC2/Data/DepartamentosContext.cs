using Microsoft.EntityFrameworkCore;
using MvcCoreAWSPostgresEC2.Models;

namespace MvcCoreAWSPostgresEC2.Data
{
    public class DepartamentosContext : DbContext
    {
        public DepartamentosContext(DbContextOptions<DepartamentosContext> options) : base(options) { }

        public DbSet<Departamento> Departamentos { get; set; }
    }
}
