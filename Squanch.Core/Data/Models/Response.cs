using System.Collections.Generic;

namespace Squanch.Core.Data.Models
{
    class Response<TResponse>
    {
        public ResponseInfo Info { get; set; }
        public List<TResponse> Results { get; set; }
    }
}
