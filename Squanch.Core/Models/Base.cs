using System;

namespace Squanch.Core.Models
{
    public class Base
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public DateTimeOffset Created { get; set; }
    }
}
