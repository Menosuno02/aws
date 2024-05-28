using MvcPeliculasApiAWS.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace MvcPeliculasApiAWS.Services
{
    public class ServiceApiPeliculas
    {
        private string UrlApi;
        private MediaTypeWithQualityHeaderValue header;

        public ServiceApiPeliculas(IConfiguration configuration)
        {
            this.UrlApi = configuration.GetValue<string>("ApiUrls:ApiPeliculasAWS");
            this.header = new MediaTypeWithQualityHeaderValue("application/json");
        }

        private async Task<T> CallApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(header);
                HttpResponseMessage response = await client.GetAsync(this.UrlApi + request);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }

        public async Task<List<Pelicula>> GetPeliculasAsync()
        {
            string request = "api/Peliculas";
            List<Pelicula> data = await CallApiAsync<List<Pelicula>>(request);
            return data;
        }

        public async Task<List<Pelicula>> GetPeliculasActorAsync(string actor)
        {
            string request = "api/Peliculas/Find/" + actor;
            List<Pelicula> data = await CallApiAsync<List<Pelicula>>(request);
            return data;
        }

        public async Task<Pelicula> FindPeliculaAsync(int id)
        {
            string request = "api/Peliculas/" + id;
            Pelicula data = await CallApiAsync<Pelicula>(request);
            return data;
        }

        public async Task CreatePeliculaAsync(Pelicula pelicula)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/Peliculas";
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(header);
                string json = JsonConvert.SerializeObject(pelicula);
                StringContent content = new StringContent(json, this.header);
                HttpResponseMessage response = await client.PostAsync(this.UrlApi + request, content);
            }
        }

        public async Task UpdatePeliculaAsync(Pelicula pelicula)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/Peliculas";
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(header);
                string json = JsonConvert.SerializeObject(pelicula);
                StringContent content = new StringContent(json, this.header);
                HttpResponseMessage response = await client.PutAsync(this.UrlApi + request, content);
            }
        }

        public async Task DeletePeliculaAsync(int idpelicula)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/Peliculas/" + idpelicula;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(header);
                HttpResponseMessage response = await client.DeleteAsync(this.UrlApi + request);
            }
        }
    }
}
