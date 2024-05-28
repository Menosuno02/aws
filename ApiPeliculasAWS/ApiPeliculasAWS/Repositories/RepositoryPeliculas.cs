using ApiPeliculasAWS.Data;
using ApiPeliculasAWS.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiPeliculasAWS.Repositories
{
    public class RepositoryPeliculas
    {
        private PeliculasContext context;

        public RepositoryPeliculas(PeliculasContext context)
        {
            this.context = context;
        }

        public async Task<List<Pelicula>> GetPeliculasAsync()
        {
            return await this.context.Peliculas.ToListAsync();
        }

        public async Task<List<Pelicula>> GetPeliculasActoresAsync(string actor)
        {
            return await this.context.Peliculas
                .Where(p => p.Actores.Contains(actor))
                .ToListAsync();
        }

        public async Task<Pelicula> FindPeliculaAsync(int idPelicula)
        {
            return await this.context.Peliculas
                .FirstOrDefaultAsync(p => p.IdPelicula == idPelicula);
        }

        private async Task<int> GetMaxIdPeliculaAsync()
        {
            return await this.context.Peliculas
                .MaxAsync(p => p.IdPelicula) + 1;
        }

        public async Task CreatePeliculaAsync(string genero, string titulo, string argumento, string foto, string actores, int precio, string youtube)
        {
            Pelicula pelicula = new Pelicula
            {
                IdPelicula = await GetMaxIdPeliculaAsync(),
                Genero = genero,
                Titulo = titulo,
                Argumento = argumento,
                Foto = foto,
                Actores = actores,
                Precio = precio,
                Youtube = youtube
            };
            await this.context.Peliculas.AddAsync(pelicula);
            await this.context.SaveChangesAsync();
        }

        public async Task UpdatePeliculaAsync(int idPelicula, string genero, string titulo, string argumento, string foto, string actores, int precio, string youtube)
        {
            Pelicula pelicula = await this.FindPeliculaAsync(idPelicula);
            pelicula.Genero = genero;
            pelicula.Titulo = titulo;
            pelicula.Argumento = argumento;
            pelicula.Foto = foto;
            pelicula.Actores = actores;
            pelicula.Precio = precio;
            pelicula.Youtube = youtube;
            await this.context.SaveChangesAsync();
        }

        public async Task DeletePeliculaAsync(int idPelicula)
        {
            Pelicula pelicula = await this.FindPeliculaAsync(idPelicula);
            this.context.Peliculas.Remove(pelicula);
            await this.context.SaveChangesAsync();
        }
    }
}
