using System;
using AzisFood.MQ.Abstractions.Models;

namespace AzisFood.MQ.Abstractions.Attributes
{
    /// <summary>
    /// Sets bus topic info for class
    /// </summary>
    public class BusTopic : Attribute
    {
        /// <summary>
        /// Topic name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Topic name with prefix
        /// </summary>
        public string FullName(string prefix, bool modifier = true) => $"{(modifier ? "queue:" : "")}{prefix.ToLowerInvariant()}.{Name}";

        /// <summary>
        /// Produced events
        /// </summary>
        public EventType[] Events { get; set; } = {EventType.Recache};
    }
}