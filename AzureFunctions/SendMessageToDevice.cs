using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SharedLibrares.Models;
using SharedLibrares.Services;
using Microsoft.Azure.Devices;

namespace AzureFunctions
{
    public static class SendMessageToDevice
    {

        private static readonly ServiceClient serviceClient = ServiceClient.CreateFromConnectionString(Environment.GetEnvironmentVariable("IotHubConnection")); 

        [FunctionName("SendMessageToDevice")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            //QueryString Localhost:7071/api/sendmessagetoderice?targetdeviceid=consoleapp&message=dettaarmeddelandet
            string targetDeviceId = req.Query["targetdeviceid"];
            string message = req.Query["message"];

            //Http Body = {"targetdericeid": "consoleapp", "message": "Detta är ett meddelande"}
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();


            var data = JsonConvert.DeserializeObject<BodyMessageModel>(requestBody);
            targetDeviceId = targetDeviceId ?? data?.TargetDeviceId;

            message = message ?? data?.Message;
          await  DeviceService.SendMessageToDeviceAsync(serviceClient, targetDeviceId, message);

            

            return new OkResult();
        }
    }
}
