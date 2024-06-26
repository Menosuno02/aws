﻿using Amazon.SQS;
using Microsoft.Extensions.DependencyInjection;
using ReceiverSQSConsole;
using System.Runtime.CompilerServices;

Console.WriteLine("Receiver SQS");
var serviceProvider = new ServiceCollection()
    .AddAWSService<IAmazonSQS>()
    .AddTransient<ServiceSQS>()
    .BuildServiceProvider();

ServiceSQS service = serviceProvider
    .GetService<ServiceSQS>();

List<Mensaje> mensajes = await service.ReceiveMessagesAsync();
if (mensajes == null)
{
    Console.WriteLine("No existen mensajes en Queue");
}
else
{
    Console.WriteLine("Número de mensajes: " + mensajes.Count);
    foreach (Mensaje msj in mensajes)
    {
        Console.WriteLine("-------------------------");
        Console.WriteLine("Asunto: " + msj.Asunto);
        Console.WriteLine("Email: " + msj.Email);
        Console.WriteLine("Contenido: " + msj.Contenido);
        Console.WriteLine("Fecha: " + msj.Fecha);
        await service.DeleteMessageAsync(msj.ReceiptHandle);
        Console.WriteLine("-------------------------");
    }
    Console.WriteLine("Fin lectura mensajes");
}
Console.WriteLine("Fin programa");