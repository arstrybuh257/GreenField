using System;
using System.Threading;
using System.Threading.Tasks;
using GreenField.Common.Messaging;
using GreenField.IoT.Interfaces;

namespace GreenField.IoT.Utils
{
    public class BackgroundCheckWorker
    {
        private readonly IRabbitMqBus _bus;
        private readonly IDataProvider _dataProvider;

        public BackgroundCheckWorker(IRabbitMqBus bus, IDataProvider dataProvider)
        {
            _bus = bus;
            _dataProvider = dataProvider;
        }

        public void DoWork()
        {
            Thread myThread = new Thread(new ThreadStart(GetMetrics));
            myThread.Start(); 
        }

        private void GetMetrics()
        {
            while (true)
            {
                var fields = _dataProvider.GetFieldsAsync().Result;
            
                foreach (var field in fields)
                {
                    CheckHelper.CheckPets(field, _dataProvider, _bus);
                    CheckHelper.CheckWeeds(field, _dataProvider, _bus);
                }

                Console.WriteLine("Message was sent");
                Thread.Sleep(10000);
            }
        }
    }
}