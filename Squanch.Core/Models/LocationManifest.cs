using System;
using System.Collections.Generic;
using System.Text;

namespace Squanch.Core.Models
{
    class LocationManifest
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public LocationManifest()
        {
        }

        public LocationManifest(string name, string url)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Url = url ?? throw new ArgumentNullException(nameof(url));
        }

    }
}
