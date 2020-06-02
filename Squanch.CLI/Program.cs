using Squanch.Core.Data;
using Squanch.Core.Data.Models;
using Squanch.Core.Models;
using Squanch.CLI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Squanch.CLI
{
    class Program
    {
        private const int ENTRIES_PER_PAGE = 20;
        private static readonly Repository _repository = Repository.Instance;


        static async Task Main()
        {
            Console.Clear();
            string prompt = "Welcome to Squanch \nChoose an option: \n1. View all data \n\nYour choice: ";

            int choice = InputUtils.ReadSignedInt(prompt: prompt, min: 1, max: 3);

            await (choice switch
            {
                1 => ViewSerialData(),
                _ => default
            });
        }

        static async Task ViewSerialData()
        {
            Console.Clear();
            string prompt = "Welcome to Squanch \nChoose an option: \n1. View all characters \n2. View all episodes \n3. View all locations \n0. Back\n\nYour choice: ";

            int choice = InputUtils.ReadSignedInt(prompt: prompt, min:0, max: 3);

            await (choice switch
            {
                0 => Main(),
                1 => DisplayCharacters(),
                2 => DisplayEpisodes(),
                3 => DisplayLocations(),
                _ => default
            });
        }

        static async Task DisplayCharacters(int page = 1)
        {
            Console.Clear();

            List<Character> characters;
            PageInfo info;

            Console.WriteLine("Viewing all characters");
            (characters, info) = await _repository.GetCharacters(page: page);
            
            string choice = GetListInput(characters.Cast<Base>().ToList(), page, info, out int start, out int end);

            switch (choice)
            {
                case "0": 
                    await ViewSerialData();
                    break;
                case string str when int.TryParse(str, out int parsed) && parsed >= start && parsed <= end:
                    int index = parsed - ((ENTRIES_PER_PAGE * (page - 1)) + 1);
                    DisplayEntity<Character>(characters[index]);
                    break;
                case "*":
                    await DisplayCharacters(--page);
                    break;
                case "#":
                    await DisplayCharacters(++page);
                    break;
            };
        }

        static async Task DisplayEpisodes(int page = 1)
        {
            Console.Clear();

            List<Episode> episodes;
            PageInfo info;

            Console.WriteLine("Viewing all episodes");
            (episodes, info) = await _repository.GetEpisodes(page: page);

            string choice = GetListInput(episodes.Cast<Base>().ToList(), page, info, out int start, out int end);

            switch (choice)
            {
                case "0":
                    await ViewSerialData();
                    break;
                case var str when int.TryParse(str, out int parsed) && parsed >= start && parsed <= end:
                    int index = parsed - ((ENTRIES_PER_PAGE * (page - 1)) + 1);
                    DisplayEntity<Episode>(episodes[index]);
                    break;
                case "*":
                    await DisplayEpisodes(--page);
                    break;
                case "#":
                    await DisplayEpisodes(++page);
                    break;
            };
        }

        static async Task DisplayLocations(int page = 1)
        {
            Console.Clear();

            List<Location> locations;
            PageInfo info;

            Console.WriteLine("Viewing all locations");
            (locations, info) = await _repository.GetLocations(page: page);

            string choice = GetListInput(locations.Cast<Base>().ToList(), page, info, out int start, out int end);

            switch (choice)
            {
                case "0":
                    await ViewSerialData();
                    break;
                case var str when int.TryParse(str, out int parsed) && parsed >= start && parsed <= end:
                    int index = parsed - ((ENTRIES_PER_PAGE * (page - 1)) + 1);
                    DisplayEntity<Location>(locations[index]);
                    break;
                case "*":
                    await DisplayLocations(--page);
                    break;
                case "#":
                    await DisplayLocations(++page);
                    break;
            };
        }  

        private static void DisplayEntity<T>(T entity) 
        {
            Console.Clear();

            string name, data;
            (name, data) = entity switch
            {
                Character x => ("characters", $"Name: \t{x.Name} \n" + 
                    $"Status: \t{x.Status} \n" + 
                    $"Species: \t{x.Species} \n" + 
                    $"Type: \t{x.Type} \n" + 
                    $"Gender: \t{x.Gender} \n" +
                    $"Origin: \t{x.Origin.Name} \n" +
                    $"Location: \t{x.Location.Name}"),

                Episode x => ("episodes", $"Name: \t{x.Name} \n" +
                    $"Air Date: \t{x.AirDate}"), 

                Location x => ("locations", $"Name: \t{x.Name}" +
                    $"Dimension: \t{x.Dimension}"),
                    
                _ => default
            };
            
            Console.WriteLine($"Viewing all {name}\n\n {data}");
        }

        private static string GetListInput(List<Base> entries, int page, PageInfo info, out int start, out int end) 
        {
            start = ((page - 1) * ENTRIES_PER_PAGE) + 1;
            end = (start + entries.Count) - 1;
            int i = start;

            string prompt = $"Page {page} of {info.Pages}\n\n";
            prompt += $"{string.Concat(entries.Select(x => $"{i++}: {x.Name}\n"))}\n";
            prompt += GetListNavigation(info, out List<string> specialChars);
            prompt += "\nYour choice: ";

            specialChars.Add("0");
            return InputUtils.ReadSignedIntOrSpecial(prompt: prompt, min: start, max: end, specialChars: specialChars.ToArray());
        }

        private static string GetListNavigation(PageInfo info, out List<string> specialChars) {
            specialChars = new List<string>(2);
            string navigation = "0. Back\n";
            if (!string.IsNullOrEmpty(info.Prev))
            {
                navigation += "*: Prev page\n";
                specialChars.Add("*");
            }

            if (!string.IsNullOrEmpty(info.Next))
            {
                navigation += "#: Next page\n";
                specialChars.Add("#");
            }

            return navigation;
        }
    }
}