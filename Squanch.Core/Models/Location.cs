using System;
using System.Collections.Generic;

namespace Squanch.Core.Models
{
    public class Location : Base
    {
        public string Type { get; set; }
        public string Dimension { get; set; }
        public List<string> Residents { get; set; }
    }
}
