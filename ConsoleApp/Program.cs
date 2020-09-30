using SharedLibrares.Models;
using SharedLibrares.Services;
using System;
using Microsoft.Azure.Devices.Client;

namespace ConsoleApp
{
    class Program
    {
        private static readonly string _conn = "HostName=HubbForOne.azure-devices.net;DeviceId=ConsoleApp;SharedAccessKey=u3bFrWZUgnQq++BMbw/uDQYOQPU4PPEWGBm5f5SEX1w=";



        private static readonly DeviceClient deviceClient = DeviceClient.CreateFromConnectionString(_conn, TransportType.Mqtt);

        static void Main(string[] args)
        {



            DeviceService.SendMessageAsync(deviceClient).GetAwaiter();
            DeviceService.ReceiveMessageAsync(deviceClient).GetAwaiter();


            Console.ReadKey();

        }
    }
}
