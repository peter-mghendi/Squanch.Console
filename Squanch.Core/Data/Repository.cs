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
        private readonly HttpDataService httpDataService = new HttpDataService("https://rickandmortyapi.com/api");
        private static object _lock = new object();
        private static Repository _instance;

        public static Repository Instance
        {
            get
            {
                lock (_lock)
                {
                    return _instance ??= new Repository();
                }
            }
        }

        public async Task<(List<Character>, PageInfo)> GetCharacters(string name = default, Character.CharacterStatus characterStatus = Character.CharacterStatus.Any,
            string species = default, string type = default, Character.CharacterGender characterGender = Character.CharacterGender.Any)
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

            var response = await httpDataService.GetAsync<Response<List<Character>>>("character", criteria);

            return (response.Results, response.Info);
        }

        public async Task<(List<Episode>, PageInfo)> GetEpisodes(string name = null, string episode = null)
        {
            Dictionary<string, string> criteria = new Dictionary<string, string>();
            criteria.AddIfNotNull("name", name);
            criteria.AddIfNotNull("episode", episode);

            var response = await httpDataService.GetAsync<Response<List<Episode>>>("episode", criteria);

            return (response.Results, response.Info);
        }

        public async Task<(List<Location>, PageInfo)> GetLocations(string name = null, string type = null, string dimension = null)
        {
            Dictionary<string, string> criteria = new Dictionary<string, string>();
            criteria.AddIfNotNull("name", name);
            criteria.AddIfNotNull("type", type);
            criteria.AddIfNotNull("dimension", dimension);

            var response = await httpDataService.GetAsync<Response<List<Location>>>("location", criteria);

            return (response.Results, response.Info);
        }

        public T GetFromUri<T>(string uri) => httpDataService.GetFromFullUriAsync<T>(uri).Result;
    }
}