using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEditor;

namespace Catan.Unity.Helpers
{
    public static class JsonHelper
    {
        public static int GetInt(JToken data, string field)
        {
            var token = data[field];

            if (token == null)
                throw new Exception($"Missing field: {field}");

            return token.Value<int>();
        }

        public static List<int> GetIntList(JToken data, string field)
        {
            var token = data[field];

            if (token == null)
                throw new Exception($"Missing field: {field}");

            return token.ToObject<List<int>>();
        }

        public static string GetString(JToken data, string field)
        {
            var token = data[field];

            if (token == null)
                throw new Exception($"Missing field: {field}");

            return token.Value<string>();
        }

        public static bool GetBool(JToken data, string field)
        {
            var token = data[field];

            if (token == null)
                throw new Exception($"Missing field: {field}");

            return token.Value<bool>();
        }

        public static T GetEnum<T>(JToken data, string field) where T : struct
        {
            var token = data[field];

            if (token == null)
                throw new Exception($"Missing field: {field}");

            return token.ToObject<T>();
        }

        public static T? GetNullableEnum<T>(JToken data, string field) where T : struct
        {
            var token = data[field];

            if (token == null || token.Type == JTokenType.Null)
                return null;

            return token.ToObject<T>();
        }
    }
}
