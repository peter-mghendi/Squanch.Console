using System.Collections.Generic;

namespace Squanch.Core.Data.Models
{
    class Response<TResponse>
    {
        public PageInfo Info { get; set; }
        public TResponse Results { get; set; }
    }
}
