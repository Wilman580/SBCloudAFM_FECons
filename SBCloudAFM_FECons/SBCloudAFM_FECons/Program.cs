using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using SBCloudAFM_FECons.Entities;

namespace SBCloudAFM_FECons
{
    class Program
    {
        const string SBConnectionString = "Endpoint=sb://sbcloud-afm.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=iWoY+fomhRSsoLHZikXH3YqA1qEqy2u06iDwAXyO7eo=";
        //Cola
        const string queueName = "afm-queues";
        static IQueueClient queueClient;
        //aqui la variable a retornar
        Boolean aceptado;
        static void Main(string[] args)
        {
            VerificarFE().GetAwaiter().GetResult();
        }
        static async Task VerificarFE()
        {
            queueClient = new QueueClient(SBConnectionString, queueName);
            Console.WriteLine("///////////////////////////////////////////");
            Console.WriteLine("//  RECEPTOR DE FACTURA ELECTRÓNICA AFM  //");
            Console.WriteLine("///////////////////////////////////////////");
            RegistrarEvento();
            Console.ReadKey();
            await queueClient.CloseAsync();
        }
        static void RegistrarEvento()
        {
            var opciones = new MessageHandlerOptions(FuncionError)
            {
                MaxConcurrentCalls = 1,
                AutoComplete=false
            };
            queueClient.RegisterMessageHandler(ProcesadorPaq, opciones);
        }
        static async Task ProcesadorPaq(Message msj, CancellationToken token)
        {
            //Recibir paquete
            var objetoFE = Encoding.UTF8.GetString(msj.Body);
            try
            {
                //Procesar Paquete
                var paqueteFE = Newtonsoft.Json.JsonConvert.DeserializeObject<Factura>(objetoFE);
                String cliente = paqueteFE.Cliente;
                Console.WriteLine($"Cliente: {cliente}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Se produjo un error: "+ex.Message);
            }
            await queueClient.CompleteAsync(msj.SystemProperties.LockToken);
        }

        static Task FuncionError(ExceptionReceivedEventArgs ar)
        {
            Console.WriteLine("Excepciones encontradas: "+ar.Exception);
            var contexto = ar.ExceptionReceivedContext;
            Console.WriteLine("Información");
            Console.WriteLine($"Endpoint: {contexto.Endpoint}");
            Console.WriteLine($"EntityPath: {contexto.EntityPath}");
            Console.WriteLine($"Cliente: {contexto.ClientId}");
            Console.WriteLine($"Actividad: {contexto.Action}");
            return Task.CompletedTask;
        }
        static bool verificarRUCCI(int cedula)
        {
            bool res = false;

            return res;
        }
    }
}
