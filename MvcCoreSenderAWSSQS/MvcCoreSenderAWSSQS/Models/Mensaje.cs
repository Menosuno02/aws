using System.Data;

namespace MvcCoreSenderAWSSQS.Models
{
    public class Mensaje
    {
        public string Asunto { get; set; }
        public string Email { get; set; }
        public string Contenido { get; set; }
        public DateTime Fecha { get; set; }
    }
}
