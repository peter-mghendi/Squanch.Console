using System;
using System.Collections.Generic;
using System.Text;

namespace Squanch.Core.Models
{
    public class Location : Base
    {
        public string Type { get; set; }
        public string Dimension { get; set; }
        public List<string> Residents { get; set; }

        public Location()
        {
        }

        public Location(int iD, string name, string url, DateTimeOffset created, string type, string dimension,
            List<string> residents) : base(iD, name, url, created)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Dimension = dimension ?? throw new ArgumentNullException(nameof(dimension));
            Residents = residents ?? throw new ArgumentNullException(nameof(residents));
        }
    }
}
