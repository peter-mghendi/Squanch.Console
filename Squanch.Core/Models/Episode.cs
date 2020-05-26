using System;
using System.Collections.Generic;

namespace Squanch.Core.Models
{
    public class Episode : Base
    {
        public string AirDate { get; set; }
        public List<string> Characters { get; set; }
    }
}
