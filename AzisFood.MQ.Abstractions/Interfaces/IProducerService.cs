using System.Threading;
using System.Threading.Tasks;
using AzisFood.MQ.Abstractions.Models;

namespace AzisFood.MQ.Abstractions.Interfaces
{
    public interface IProducerService<T>
    {
        /// <summary>
        /// Send signal to RabbitMQ
        /// </summary>
        /// <param name="source">Source method</param>
        /// <param name="eventType">Type of event</param>
        /// <param name="payload">Payload for signal</param>
        /// <param name="token">Token for operation cancel</param>
        Task SendEvent([System.Runtime.CompilerServices.CallerMemberName] string source = "",
            EventType eventType = EventType.Recache,
            object payload = null, CancellationToken token = default);
    }
}