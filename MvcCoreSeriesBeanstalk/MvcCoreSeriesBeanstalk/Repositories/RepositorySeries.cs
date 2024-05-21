using Microsoft.EntityFrameworkCore;
using MvcCoreSeriesBeanstalk.Data;
using MvcCoreSeriesBeanstalk.Models;

namespace MvcCoreSeriesBeanstalk.Repositories
{
    public class RepositorySeries
    {
        private SeriesContext context;

        public RepositorySeries(SeriesContext context)
        {
            this.context = context;
        }

        public async Task<List<Serie>> GetSeriesAsync()
        {
            return await this.context.Series.ToListAsync();
        }

        private async Task<int> GetMaxIdSerie()
        {
            return await this.context.Series
                .MaxAsync(s => s.IdSerie) + 1;
        }

        public async Task CreateSerieAsync(string nombre, string imagen, int anyo)
        {
            Serie serie = new Serie
            {
                IdSerie = await GetMaxIdSerie(),
                Nombre = nombre,
                Imagen = imagen,
                Anyo = anyo
            };
            await this.context.Series.AddAsync(serie);
            await this.context.SaveChangesAsync();
        }
    }
}
