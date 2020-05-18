using System;
using System.Collections.Generic;
using System.Text;
using Squanch.Core.Data.Models;
using Squanch.Core.Helpers;
using Squanch.Core.Models;
using Squanch.Core.Services;

namespace Squanch.Core.Data
{
    class Repository
    {
        private readonly object _padlock;
        private Repository _instance;
        public Repository Instance
        {
            get
            {
                lock (_padlock)
                {
                    return _instance ??= new Repository();
                }
            }
        }

        private readonly HttpDataService httpDataService = new HttpDataService("https://rickandmortyapi.com/api");

        public List<Character> Characters { get => httpDataService.GetAsync<Response<List<Character>>>("character").Result.Results; }

        public List<Episode> Episodes { get => httpDataService.GetAsync<Response<List<Episode>>>("episode").Result.Results; }

        public List<Location> Locations { get => httpDataService.GetAsync<Response<List<Location>>>("location").Result.Results; }

        public List<Character> GetFilteredCharacters(string name = null, Character.CharacterStatus characterStatus = Character.CharacterStatus.Any,
            string species = null, string type = null, Character.CharacterGender characterGender = Character.CharacterGender.Any)
        {
            string status = characterStatus != Character.CharacterStatus.Any ? 
                new[] { "alive", "dead", "unknown" }[(int)(characterStatus + 1)] : null;

            string gender = characterGender != Character.CharacterGender.Any ? 
                new[] { "male", "female", "genderless", "unknown" }[(int)(characterGender + 1)] : null;

            Dictionary<string, string> criteria = new Dictionary<string, string>();
            criteria.AddIfNotNull("name", name);
            criteria.AddIfNotNull("status", status);
            criteria.AddIfNotNull("species", species);
            criteria.AddIfNotNull("type", type);
            criteria.AddIfNotNull("gender", gender);            

            return httpDataService.GetAsync<Response<List<Character>>>("character", criteria).Result.Results;
        }

        public List<Episode> GetFilteredEpisodes(string name = null, string episode = null)
        {
            Dictionary<string, string> criteria = new Dictionary<string, string>();
            criteria.AddIfNotNull("name", name);
            criteria.AddIfNotNull("episode", episode);

            return httpDataService.GetAsync<Response<List<Episode>>>("episode", criteria).Result.Results;
        }

        public List<Location> GetFilteredLocations(string name = null, string type = null, string dimension = null)
        {
            Dictionary<string, string> criteria = new Dictionary<string, string>();
            criteria.AddIfNotNull("name", name);
            criteria.AddIfNotNull("type", type);
            criteria.AddIfNotNull("dimension", dimension);

            return httpDataService.GetAsync<Response<List<Location>>>("location", criteria).Result.Results;
        }

        public T GetFromUri<T>(string uri) => httpDataService.GetAsync<T>(uri).Result;
    }
}