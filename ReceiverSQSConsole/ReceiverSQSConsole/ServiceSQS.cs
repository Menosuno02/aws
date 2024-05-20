using Amazon.SQS;
using Amazon.SQS.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ReceiverSQSConsole
{
    public class ServiceSQS
    {
        private IAmazonSQS clientSQS;
        private string UrlQueue;

        public ServiceSQS(IAmazonSQS client)
        {
            this.clientSQS = client;
            // this.UrlQueue = "https://sqs.us-east-1.amazonaws.com/730335394157/queue-lunes-alejo";
            this.UrlQueue = "https://sqs.us-east-1.amazonaws.com/730335394157/queue-lunes-fifo-alejo.fifo";
        }

        public async Task<List<Mensaje>> ReceiveMessagesAsync()
        {
            // CUANDO REALIZAMOS UN SONDEO, DEBEMOS INDICAR EL
            // TIEMPO DE SONDEO Y EL NÚMERO DE MENSAJES A
            // RECUPERAR
            ReceiveMessageRequest request = new ReceiveMessageRequest
            {
                QueueUrl = this.UrlQueue,
                MaxNumberOfMessages = 5,
                WaitTimeSeconds = 5
            };
            ReceiveMessageResponse response = await this.clientSQS.ReceiveMessageAsync(request);
            if (response.HttpStatusCode == HttpStatusCode.OK)
            {
                if (response.Messages.Count != 0)
                {
                    // TENEMOS UNA COLECCION LLAMADA Messages
                    // QUE CONTIENE TODAS LAS PROPIEDADES DE UN MENSAJE
                    List<Message> messages = response.Messages;
                    // CREAMOS NUESTRA COLECCION PARA DEVOLVER
                    // LOS DATOS EXTRAIDOS
                    List<Mensaje> output = new List<Mensaje>();
                    foreach(Message msj in messages)
                    {
                        // DENTRO DE UN MENSAJE TENEMOS VARIAS
                        // CARACTERÍSTICAS
                        string json = msj.Body;
                        Mensaje data = JsonConvert.DeserializeObject<Mensaje>(json);
                        data.ReceiptHandle = msj.ReceiptHandle;
                        output.Add(data);
                    }
                    return output;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public async Task DeleteMessageAsync(string receiptHandle)
        {
            DeleteMessageRequest request = new DeleteMessageRequest
            {
                QueueUrl = this.UrlQueue,
                ReceiptHandle = receiptHandle
            };
            DeleteMessageResponse response = await this.clientSQS.DeleteMessageAsync(request);
            HttpStatusCode statusCode = response.HttpStatusCode;
        }
    }
}
