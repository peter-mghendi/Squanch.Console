using System;
using System.Collections.Generic;
using System.Text;

namespace Squanch.Core.Data.Models
{
    class ResponseInfo
    {
        public int Count { get; set; }
        public int Pages { get; set; }
        public string Next { get; set; }
        public string Prev { get; set; }
    }
}
