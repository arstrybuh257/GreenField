using GreenField.Common.Enums;
using GreenField.Common.Messaging;
using GreenField.Common.Messaging.Messages;
using GreenField.IoT.DataGenerators;
using GreenField.IoT.Interfaces;
using GreenField.IoT.Models;

namespace GreenField.IoT.Utils
{
    public class CheckHelper
    {
        public static  void CheckPets(Field field, IDataProvider dataProvider, IRabbitMqBus bus)
        {
            var result = PestCheckDataGenerator.Generate(dataProvider.GetPestsAsync().Result);

            if (result != null)
            {
                var severity = DangerLevel.Low;
                if (result.CountOnSquareMeter >= 50 && result.CountOnSquareMeter < 100)
                {
                    severity = DangerLevel.High;
                }
                else if (result.CountOnSquareMeter > 100)
                {
                    severity = DangerLevel.Critical;
                }
                    
                var message = new PestDetectedMessage()
                {
                    FieldId = field.Id,
                    OrganizationId = field.OrganizationId,
                    PestId = result.PestId,
                    CountOnSquareMeter = result.CountOnSquareMeter,
                    Severity = severity
                };
                    
                bus.Publish(message);
            }
        }

        public static void CheckWeeds(Field field, IDataProvider dataProvider, IRabbitMqBus bus)
        {
            var result = WeedCheckDataGenerator.Generate(dataProvider.GetWeedsAsync().Result);

            if (result != null)
            {
                var severity = DangerLevel.Low;
                if (result.Percentage >= 10 && result.Percentage <= 60)
                {
                    severity = DangerLevel.High;
                }
                else if (result.Percentage > 60)
                {
                    severity = DangerLevel.Critical;
                }
                    
                var message = new WeedDetectedMessage()
                {
                    FieldId = field.Id,
                    OrganizationId = field.OrganizationId,
                    WeedId = result.WeedId,
                    Percentage = result.Percentage,
                    Severity = severity
                };
                    
                bus.Publish(message);
            }
        }
    }
}