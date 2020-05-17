using System;
using System.Collections.Generic;
using System.Text;

namespace Squanch.Core.Helpers
{
    static class Extensions
    {
        public static void AddIfNotNull(this Dictionary<string, string> pairs, string key, string value)
        {
            if (!!string.IsNullOrEmpty(value)) pairs.Add(key, value);
        }
    }
}

