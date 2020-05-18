using System;
using System.Collections.Generic;
using System.Text;

namespace Squanch.Core.Models
{
    public class Character : Base
    {
        public string Status { get; set; }
        public string Species { get; set; }
        public string Type { get; set; }
        public string Gender { get; set; }
        public LocationManifest Origin { get; set; }
        public LocationManifest Location { get; set; }
        public string Image { get; set; }
        public List<string> Episodes { get; set; }

        public enum CharacterStatus
        {
            Any,
            Alive, 
            Dead,
            Unknown
        }

        public enum CharacterGender
        {
            Any,
            Male,
            Female,
            Genderless,
            Unknown
        }

        public Character()
        {
        }

        public Character(int iD, string name, string url, DateTimeOffset created, string status, string species, string type,
            string gender, LocationManifest origin, LocationManifest location, string image) : base(iD, name, url, created)
        {
            Status = status ?? throw new ArgumentNullException(nameof(status));
            Species = species ?? throw new ArgumentNullException(nameof(species));
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Gender = gender ?? throw new ArgumentNullException(nameof(gender));
            Origin = origin ?? throw new ArgumentNullException(nameof(origin));
            Location = location ?? throw new ArgumentNullException(nameof(location));
            Image = image ?? throw new ArgumentNullException(nameof(image));
        }
    }
}