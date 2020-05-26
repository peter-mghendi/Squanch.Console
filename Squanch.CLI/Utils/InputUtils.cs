using System;

namespace Squanch.CLI.Utils
{
    public static class InputUtils 
    {
        private const string defaultErrorPrompt = "ERROR! Invalid Value. Try again: ";

        public static int ReadSignedInt
        (
            string prompt = "Enter an Int32: ",
            string errorPrompt = defaultErrorPrompt,  
            int min = int.MinValue,
            int max = int.MaxValue, 
            bool error = false
        ) 
        {
            Console.Write(error? errorPrompt: prompt);
            return (!int.TryParse(Console.ReadLine(), out int output) || output < min || output > max) 
                ? ReadSignedInt(prompt, errorPrompt, min, max, true)
                : output;
        }

        public static uint ReadUnsignedInt
        (
            string prompt = "Enter a UInt32: ",
            string errorPrompt = defaultErrorPrompt,  
            uint min = uint.MinValue,
            uint max = uint.MaxValue, 
            bool error = false
        ) 
        {
            Console.Write(error? errorPrompt: prompt);
            return (!uint.TryParse(Console.ReadLine(), out uint output) || output < min || output > max) 
                ? ReadUnsignedInt(prompt, errorPrompt, min, max, true)
                : output;
        }

        public static string ReadSignedIntOrSpecial
        (
            string prompt = "Enter an Int32: ",
            string errorPrompt = defaultErrorPrompt,
            int min = int.MinValue,
            int max = int.MaxValue,
            string[] specialChars = default,
            bool error = false
        )
        {
            Console.Write(error ? errorPrompt : prompt);

            string input = Console.ReadLine().Trim();

            if (Array.Exists(specialChars, x => x == input))
                return input;

            if (int.TryParse(input, out int output) && output >= min && output <= max)
                return input;

            return ReadSignedIntOrSpecial(prompt, errorPrompt, min, max, specialChars, true);
        }

        public static string ReadUnsignedIntOrSpecial
        (
            string prompt = "Enter a UInt32: ",
            string errorPrompt = defaultErrorPrompt,
            uint min = uint.MinValue,
            uint max = uint.MaxValue,
            string[] specialChars = default,
            bool error = false
        )
        {
            Console.Write(error ? errorPrompt : prompt);

            string input = Console.ReadLine().Trim();

            if (Array.Exists(specialChars, x => x == input))
                return input;

            if (uint.TryParse(input, out uint output) && output >= min && output <= max)
                return input;

            return ReadUnsignedIntOrSpecial(prompt, errorPrompt, min, max, specialChars, true);
        }
    }
}