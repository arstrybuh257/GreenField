using System.Threading;
using System.Threading.Tasks;
using GreenField.Common.Messaging;
using GreenField.Common.Messaging.Messages;
using GreenField.IoT.Interfaces;
using GreenField.IoT.Utils;
using MediatR;

namespace GreenField.IoT.Handlers
{
    public class CheckFieldOnDemandHandler : INotificationHandler<CheckFieldOnDemand>
    {
        private IRabbitMqBus _bus;
        private IFieldService _fieldService;
        private IDataProvider _dataProvider;

        public CheckFieldOnDemandHandler(IRabbitMqBus bus, IFieldService fieldService, IDataProvider dataProvider)
        {
            _bus = bus;
            _fieldService = fieldService;
            _dataProvider = dataProvider;
        }

        public async Task Handle(CheckFieldOnDemand command, CancellationToken cancellationToken)
        {
            var field = await _fieldService.GetFieldAsync(command.FieldId);
            
            CheckHelper.CheckPets(field, _dataProvider, _bus);
            CheckHelper.CheckWeeds(field, _dataProvider, _bus);
        }
    }
}