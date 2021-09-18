using System;

namespace AzisFood.MQ.Abstractions.Models
{
    public class BusSignal
    {
        public DateTime SendTime { get; set; }
        public string Source { get; set; }
        public string Payload { get; set; }

        public BusSignal(string source, string payload)
        {
            SendTime = DateTime.Now;
            Source = source;
            Payload = payload;
        }
    }
}