using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Squanch.Core.Data;
using Squanch.Core.Helpers;
using Squanch.Core.Models;

namespace Squanch.CLI
{
    class Program
    {
        private static readonly Repository repository = Repository.Instance;

        static async Task Main()
        {
            string prompt = "Welcome to Squanch \nChoose an option: \n1. View all locations \n2. View all episodes \n3. View all characters\n\nYour choice: ";

            int choice = InputUtils.ReadSignedInt(prompt: prompt, min: 1, max: 3);

            await (choice switch
            {
                1 => DisplayLocations(),
                2 => DisplayEpisodes(),
                3 => DisplayCharacters(),
                _ => default // Unreachable state
            });
        }

        static async Task DisplayLocations()
        {
            int i = 0;
            List<Location> locations = await repository.GetLocations();
            locations.ForEach(location => Console.WriteLine($"{++i}: {location.Name}"));
        }

        static async Task DisplayEpisodes()
        {
            int i = 0;
            List<Episode> episodes = await repository.GetEpisodes();
            episodes.ForEach(episode => Console.WriteLine($"{++i}: {episode.Name}"));
        }

        static async Task DisplayCharacters()
        {
            int i = 0;
            List<Character> characters = await repository.GetCharacters();
            characters.ForEach(character => Console.WriteLine($"{++i}: {character.Name}"));
        }
    }
}