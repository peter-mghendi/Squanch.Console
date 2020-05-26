using System;
using System.Collections.Generic;

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
    }
}