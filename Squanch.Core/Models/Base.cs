using System;
using System.Collections.Generic;
using System.Text;

namespace Squanch.Core.Models
{
    class Base
    {
        public Base()
        {
        }

        public Base(int iD, string name, string url, DateTimeOffset created)
        {
            ID = iD;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Url = url ?? throw new ArgumentNullException(nameof(url));
            Created = created;
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public DateTimeOffset Created { get; set; }
    }
}
