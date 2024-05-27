using ApiCrudPersonajesAWS.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCrudPersonajesAWS.Data
{
    public class PersonajesContext : DbContext
    {
        public PersonajesContext(DbContextOptions<PersonajesContext> options) : base(options) { }

        public DbSet<Personaje> Personajes { get; set; }
    }
}
