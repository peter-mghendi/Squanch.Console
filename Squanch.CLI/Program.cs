using Squanch.Core.Data;
using Squanch.Core.Data.Models;
using Squanch.Core.Helpers;
using Squanch.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Squanch.CLI
{
    class Program
    {
        private static readonly Repository repository = Repository.Instance;

        static async Task Main()
        {
            string prompt = "Welcome to Squanch \nChoose an option: \n1. View all characters \n2. View all episodes \n3. View all locations\n\nYour choice: ";

            int choice = InputUtils.ReadSignedInt(prompt: prompt, min: 1, max: 3);

            await (choice switch
            {
                1 => DisplayCharacters(),
                2 => DisplayEpisodes(),
                3 => DisplayLocations(),
                _ => default // Unreachable state
            });
        }

        static async Task DisplayCharacters()
        {
            int i = 0, page = 1;

            List<Character> characters;
            PageInfo info;

            (characters, info) = await repository.GetCharacters();

            Console.WriteLine($"Page: {page} of {info.Pages}\n");
            Console.WriteLine($"{characters.Select(x => $"{++i}: {x.Name}\n")}\n");

            WriteNavigation(info);
        }

        static async Task DisplayEpisodes()
        {
            int i = 0, page = 1;

            List<Episode> episodes;
            PageInfo info;

            (episodes, info) = await repository.GetEpisodes();

            Console.WriteLine($"Page: {page} of {info.Pages}\n");
            Console.WriteLine($"{episodes.Select(x => $"{++i}: {x.Name}\n")}\n");

            WriteNavigation(info);
        }

        static async Task DisplayLocations()
        {
            int i = 0, page = 1;

            List<Location> locations;
            PageInfo info;

            (locations, info) = await repository.GetLocations();
            locations.ForEach(location => Console.WriteLine($"{++i}: {location.Name}"));

            Console.WriteLine($"Page: {page} of {info.Pages}\n");
            Console.WriteLine($"{locations.Select(x => $"{++i}: {x.Name}\n")}\n");

            WriteNavigation(info);
        }

        private static void WriteNavigation(PageInfo info) {
            if (!string.IsNullOrEmpty(info.Prev)) Console.WriteLine("*: Prev page");
            if (!string.IsNullOrEmpty(info.Next)) Console.WriteLine("#: Next page");
        }
    }
}