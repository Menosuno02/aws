using Microsoft.EntityFrameworkCore;
using MvcCoreSeriesBeanstalk.Models;

namespace MvcCoreSeriesBeanstalk.Data
{
    public class SeriesContext : DbContext
    {
        public SeriesContext(DbContextOptions<SeriesContext> options) : base(options) { }

        public DbSet<Serie> Series { get; set; }
    }
}
