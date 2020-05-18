using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Squanch.Core.Data.Models;
using Squanch.Core.Helpers;
using Squanch.Core.Models;
using Squanch.Core.Services;

namespace Squanch.Core.Data
{
    public class Repository
    {
        private static object _padlock = new object();
        private static Repository _instance;

        public static Repository Instance
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

        public async Task<List<Character>> GetCharacters(string name = null, Character.CharacterStatus characterStatus = Character.CharacterStatus.Any,
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

            return (await httpDataService.GetAsync<Response<List<Character>>>("character", criteria)).Results;
        }

        public async Task<List<Episode>> GetEpisodes(string name = null, string episode = null)
        {
            Dictionary<string, string> criteria = new Dictionary<string, string>();
            criteria.AddIfNotNull("name", name);
            criteria.AddIfNotNull("episode", episode);

            return (await httpDataService.GetAsync<Response<List<Episode>>>("episode", criteria)).Results;
        }

        public async Task<List<Location>> GetLocations(string name = null, string type = null, string dimension = null)
        {
            Dictionary<string, string> criteria = new Dictionary<string, string>();
            criteria.AddIfNotNull("name", name);
            criteria.AddIfNotNull("type", type);
            criteria.AddIfNotNull("dimension", dimension);

            return (await httpDataService.GetAsync<Response<List<Location>>>("location", criteria)).Results;
        }

        public T GetFromUri<T>(string uri) => httpDataService.GetAsync<T>(uri).Result;
    }
}