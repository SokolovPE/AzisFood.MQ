using System;
using System.Linq;
using System.Threading.Tasks;
using AzisFood.MQ.Abstractions.Attributes;
using AzisFood.MQ.Abstractions.Interfaces;
using AzisFood.MQ.Abstractions.Models;
using MassTransit;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AzisFood.MQ.Rabbit.Implementations
{
    public class ProducerService<T> : IProducerService<T>
    {
        private readonly ILogger<ProducerService<T>> _logger;
        private readonly BusTopic _busTopic;
        private readonly string _entityName;
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public ProducerService(ILogger<ProducerService<T>> logger, ISendEndpointProvider sendEndpointProvider)
        {
            _logger = logger;
            _sendEndpointProvider = sendEndpointProvider;
            var customAttributes = (BusTopic[]) typeof(T).GetCustomAttributes(typeof(BusTopic), true);
            _busTopic = customAttributes.FirstOrDefault();
            _entityName = typeof(T).Name;
        }
        public async Task SendEvent(string source = "", EventType eventType = EventType.Recache, object payload = null)
        {
            _logger.LogInformation(
                $"Requested event {eventType.ToString()} push to Message Bus from {source} with payload: {JsonConvert.SerializeObject(payload)}");
            try
            {
                if (_busTopic == null || string.IsNullOrEmpty(_busTopic.Name) ||
                    !_busTopic.Events.Contains(eventType))
                {
                    _logger.LogWarning(
                        $"Bus topic is missing or operation {eventType.ToString()} is not permitted for {_entityName}");
                    return;
                }

                var payloadJson = payload == null ? string.Empty : JsonConvert.SerializeObject(payload);
                var endpoint =
                    await _sendEndpointProvider.GetSendEndpoint(
                        new Uri(_busTopic.FullName(eventType.ToString())));
                await endpoint.Send(new BusSignal(source, payloadJson));
                _logger.LogInformation($"Event {eventType.ToString()} fired succeeded for {_entityName}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error during event push to Message Bus");
            }
        }
    }
}