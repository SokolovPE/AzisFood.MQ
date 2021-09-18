using AzisFood.MQ.Abstractions.Interfaces;

namespace AzisFood.MQ.Abstractions.Implementations
{
    public class MQOptions : IMQOptions
    {
        public string ConnectionString { get; set; }
    }
}