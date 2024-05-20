using Microsoft.AspNetCore.Mvc;
using MvcCoreSenderAWSSQS.Models;
using MvcCoreSenderAWSSQS.Services;

namespace MvcCoreSenderAWSSQS.Controllers
{
    public class MessagesSQSController : Controller
    {
        private ServiceSQS service;

        public MessagesSQSController(ServiceSQS service)
        {
            this.service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(Mensaje mensaje)
        {
            await this.service.SendMessageAsync(mensaje);
            ViewData["MENSAJE"] = "Mensaje enviado a Queue SQS";
            return View();
        }
    }
}
