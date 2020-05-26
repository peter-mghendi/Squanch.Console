using System.Collections.Generic;
using System.Threading.Tasks;
using Squanch.Core.Data.Models;
using Squanch.Core.Models;
using Squanch.Core.Services;

namespace Squanch.Core.Data
{
    public class Repository
    {
        private readonly HttpDataService httpDataService = new HttpDataService("https://rickandmortyapi.com/api");
        
        private static readonly string[] characterStatuses = new[] { "alive", "dead", "unknown" };
        private static readonly string[] characterGenders = new[] { "male", "female", "genderless", "unknown" };

        private static readonly object _lock = new object();
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

        public async Task<(List<Character>, PageInfo)> GetCharacters(uint page = 1, string name = default, Character.CharacterStatus characterStatus = Character.CharacterStatus.Any,
            string species = default, string type = default, Character.CharacterGender characterGender = Character.CharacterGender.Any)
        {
            Dictionary<string, string> criteria = new Dictionary<string, string>();

            string status = characterStatus != Character.CharacterStatus.Any ?
                characterStatuses[(int)(characterStatus + 1)] : null;

            string gender = characterGender != Character.CharacterGender.Any ?
                characterGenders[(int)(characterGender + 1)] : null;

            if (page > 1) criteria.Add("page", page.ToString());

            criteria.Add("name", name);
            criteria.Add("status", status);
            criteria.Add("species", species);
            criteria.Add("type", type);
            criteria.Add("gender", gender);

            var response = await httpDataService.GetAsync<Response<List<Character>>>("character", criteria);
            return (response.Results, response.Info);
        }

        public async Task<(List<Episode>, PageInfo)> GetEpisodes(uint page = 1, string name = default, string episode = default)
        {
            Dictionary<string, string> criteria = new Dictionary<string, string>();

            if (page > 1) criteria.Add("page", page.ToString());

            criteria.Add("name", name);
            criteria.Add("episode", episode);

            var response = await httpDataService.GetAsync<Response<List<Episode>>>("episode", criteria);
            return (response.Results, response.Info);
        }

        public async Task<(List<Location>, PageInfo)> GetLocations(uint page = 1, string name = default, string type = default, string dimension = default)
        {
            Dictionary<string, string> criteria = new Dictionary<string, string>();

            if (page > 1) criteria.Add("page", page.ToString());

            criteria.Add("name", name);
            criteria.Add("type", type);
            criteria.Add("dimension", dimension);

            var response = await httpDataService.GetAsync<Response<List<Location>>>("location", criteria);
            return (response.Results, response.Info);
        }

        public T GetFromUri<T>(string uri) => httpDataService.GetFromFullUriAsync<T>(uri).Result;
    }
}