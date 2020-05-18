using System;
using System.Collections.Generic;
using System.Text;

namespace Squanch.Core.Models
{
    public class Episode : Base
    {
        public string AirDate { get; set; }
        public List<string> Characters { get; set; }

        public Episode()
        {
        }

        public Episode(int iD, string name, string url, DateTimeOffset created, string airDate, List<string> characters) : base(iD, name, url, created)
        {
            AirDate = airDate ?? throw new ArgumentNullException(nameof(airDate));
            Characters = characters ?? throw new ArgumentNullException(nameof(characters));
        }
    }
}
